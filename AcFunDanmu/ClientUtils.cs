using Google.Protobuf;
using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace AcFunDanmu
{
    public static class ClientUtils
    {
        private const int HeaderOffset = 12;
        private const int IVLength = 16;

        internal static void Copy<T>(ReadOnlySpan<T> source, int sourceIndex, Span<T> target, int targetIndex, int length)
        {
            for (var i = 0; i < length; i++)
            {
                target[i + targetIndex] = source[i + sourceIndex];
            }
        }

        internal static (byte[], byte[]) EncodeLengths(int packetLength, int bodyLength)
        {
            Span<byte> packet = BitConverter.GetBytes(packetLength), body = BitConverter.GetBytes(bodyLength);
            if (BitConverter.IsLittleEndian)
            {
                packet.Reverse();
                body.Reverse();
            }
            return (packet.ToArray(), body.ToArray());
        }

        public static byte[] Encode(PacketHeader header, ByteString body, string key)
        {
            var bHeader = header.ToByteArray();

            var encrypt = Encrypt(key, body);

            var len = HeaderOffset + bHeader.Length + encrypt.Length;

            Span<byte> data = len < 1024 ? stackalloc byte[len] : new byte[len];
            data[0] = 0xAB;
            data[1] = 0xCD;
            data[2] = 0x00;
            data[3] = 0x01;

            var (packetLength, bodyLength) = EncodeLengths(bHeader.Length, encrypt.Length);
            Copy(packetLength, 0, data, 4, packetLength.Length);
            Copy(bodyLength, 0, data, 8, bodyLength.Length);
            Copy(bHeader, 0, data, HeaderOffset, bHeader.Length);
            Copy(encrypt, 0, data, HeaderOffset + bHeader.Length, encrypt.Length);

            return data.ToArray();
        }

        internal static (int, int) DecodeLengths(ReadOnlySpan<byte> bytes)
        {
            Span<byte> header = stackalloc byte[4], payload = stackalloc byte[4];

            bytes.Slice(4, 4).CopyTo(header);
            bytes.Slice(8, 4).CopyTo(payload);

            if (BitConverter.IsLittleEndian)
            {
                header.Reverse();
                payload.Reverse();
            }

            var headerLength = BitConverter.ToInt32(header);
            var payloadLength = BitConverter.ToInt32(payload);

            return (headerLength, payloadLength);
        }

        public static object Decode(Type type, ReadOnlySpan<byte> bytes, string SecurityKey, string SessionKey, out PacketHeader header)
        {
            var (headerLength, payloadLength) = DecodeLengths(bytes);

            header = PacketHeader.Parser.ParseFrom(bytes.Slice(HeaderOffset, headerLength).ToArray());

            ReadOnlySpan<byte> payload = bytes.Slice(HeaderOffset + headerLength, payloadLength);
            if (header.EncryptionMode != PacketHeader.Types.EncryptionMode.KEncryptionNone)
            {
                var key = header.EncryptionMode == PacketHeader.Types.EncryptionMode.KEncryptionServiceToken ? SecurityKey : SessionKey;

                payload = Decrypt(payload, key);
            }


            if (Convert.ToUInt32(payload.Length) != header.DecodedPayloadLen)
            {
#if DEBUG
                Console.WriteLine("Payload length does not match");
                Console.WriteLine(Convert.ToBase64String(payload));
#endif
                return null;
            }

            var obj = Parse(type, payload);
            return obj;
        }

        internal static byte[] Encrypt(string key, ByteString body)
        {
            using var aes = Aes.Create();

            using var encryptor = aes.CreateEncryptor(Convert.FromBase64String(key), aes.IV);
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            body.WriteTo(cs);
            cs.FlushFinalBlock();

            var encrypted = ms.ToArray();

            var len = aes.IV.Length + encrypted.Length;

            Span<byte> payload = len < 1024 ? stackalloc byte[len] : new byte[len];
            Copy(aes.IV, 0, payload, 0, aes.IV.Length);
            Copy(encrypted, 0, payload, aes.IV.Length, encrypted.Length);

            return payload.ToArray();
        }

        internal static byte[] Decrypt(ReadOnlySpan<byte> bytes, string key)
        {
            using var aes = Aes.Create();
            using var decryptor = aes.CreateDecryptor(Convert.FromBase64String(key), bytes.Slice(0, IVLength).ToArray());
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write);
            cs.Write(bytes.Slice(IVLength));
            cs.FlushFinalBlock();

            return ms.ToArray();
        }

        internal static ByteString Compress(ByteString payload)
        {
            return GZip(CompressionMode.Compress, payload);
        }

        public static ByteString Decompress(ByteString payload)
        {
            return GZip(CompressionMode.Decompress, payload);
        }

        internal static ByteString GZip(CompressionMode mode, ByteString payload)
        {
            using var input = new MemoryStream(payload.ToByteArray());
            using var gzip = new GZipStream(input, mode);
            using var output = new MemoryStream();

            gzip.CopyTo(output);

            output.Position = 0;

            return ByteString.FromStream(output);
        }

        internal static object Parse(Type type, object[] payload)
        {
            var pt = typeof(MessageParser<>).MakeGenericType(new Type[] { type });

            var parser = type.GetProperty("Parser", pt).GetValue(null);
            var method = pt.GetMethod("ParseFrom", new Type[] { typeof(ByteString) });

            var obj = method.Invoke(parser, payload);
            return obj;
        }

        public static object Parse(Type type, ReadOnlySpan<byte> payload)
        {
            return Parse(type, new object[] { ByteString.CopyFrom(payload) });
        }

        public static object Parse(string typeName, ByteString payload)
        {
            var type = Type.GetType($"AcFunDanmu.{typeName}");
            if (type != null)
            {
                return Parse(type, new object[] { payload });
            }
            else
            {
                Console.WriteLine("Unhandled type: {0}", typeName);
                Console.WriteLine(payload.ToBase64());
                return null;
            }
        }
    }
}
