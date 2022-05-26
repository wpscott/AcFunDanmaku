using AcFunDanmu.Im.Basic;
using Google.Protobuf;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace AcFunDanmu
{
    public static class ClientUtils
    {
        private static readonly SortedList<string, string> QueryDict = new SortedList<string, string>()
        {
            { "appver", "1.4.0.145" },
            { "sys", "PC_10" },
            { "kpn", "ACFUN_APP.LIVE_MATE" },
            { "kpf", "WINDOWS_PC" },
            { "subBiz", "mainApp" },
        };

        private static string Query => string.Join("&", QueryDict.Select(query => $"{query.Key}={query.Value}"));

        private const int HeaderOffset = 12;
        private const int IVLength = 16;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Copy<T>(in ReadOnlySpan<T> source, in int sourceIndex, in Span<T> target,
            in int targetIndex, in int length)
        {
            for (var i = 0; i < length; i++)
            {
                target[i + targetIndex] = source[i + sourceIndex];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CopyLength(in Length length, in Span<byte> target, in int targetIndex)
        {
            if (BitConverter.IsLittleEndian)
            {
                target[targetIndex + 0] = length.b3;
                target[targetIndex + 1] = length.b2;
                target[targetIndex + 2] = length.b1;
                target[targetIndex + 3] = length.b0;
            }
            else
            {
                target[targetIndex + 0] = length.b0;
                target[targetIndex + 1] = length.b1;
                target[targetIndex + 2] = length.b2;
                target[targetIndex + 3] = length.b3;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe (Length, Length) EncodeLengths(in int headerLength, in int payloadLength)
        {
            fixed (int* headerPtr = &headerLength, payloadPtr = &payloadLength)
            {
                var header = Unsafe.AsRef<Length>(headerPtr);
                var payload = Unsafe.AsRef<Length>(payloadPtr);

                return (header, payload);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] Encode(in PacketHeader header, in ByteString body, in string key) =>
            Encode(header, Encrypt(key, body));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] Encode(in PacketHeader header, in ReadOnlySpan<byte> body, in string key) =>
            Encode(header, Encrypt(key, body));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] Encode(in PacketHeader header, in ReadOnlySpan<byte> encrypted)
        {
            var bHeader = header.ToByteArray();

            var len = HeaderOffset + bHeader.Length + encrypted.Length;

            Span<byte> data = stackalloc byte[len];
            data[0] = 0xAB;
            data[1] = 0xCD;
            data[2] = 0x00;
            data[3] = 0x01;

            var (headerLength, payloadLength) = EncodeLengths(bHeader.Length, encrypted.Length);

            CopyLength(headerLength, data, 4);
            CopyLength(payloadLength, data, 8);
            Copy(bHeader, 0, data, HeaderOffset, bHeader.Length);
            Copy(encrypted, 0, data, HeaderOffset + bHeader.Length, encrypted.Length);

            //Dump(data);

            //Decode<UpstreamPayload>(data, key, key, out _);

            return data.ToArray();
        }

        public static void Dump(in ReadOnlySpan<byte> data)
        {
            for (var i = 0; i < data.Length; i++)
            {
                Console.Write(data[i].ToString("X2"));
                if ((i & 1) == 1)
                {
                    Console.Write(" ");
                }

                if ((i & 15) == 15)
                {
                    Console.WriteLine();
                }
            }

            Console.WriteLine();
        }

        [StructLayout(LayoutKind.Explicit, Pack = 4, Size = 4)]
        private struct Length
        {
            [FieldOffset(0)] public readonly byte b0;
            [FieldOffset(1)] public readonly byte b1;
            [FieldOffset(2)] public readonly byte b2;
            [FieldOffset(3)] public readonly byte b3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int ByteToInt32(in Length length) =>
            (length.b0 << 24) | (length.b1 << 16) | (length.b2 << 8) | (length.b3 << 0);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe (int, int) DecodeLengths(in ReadOnlySpan<byte> bytes)
        {
            fixed (byte* headerPtr = bytes)
            {
                var header = Unsafe.AsRef<Length>(headerPtr + 4);
                var payload = Unsafe.AsRef<Length>(headerPtr + 8);

                return (ByteToInt32(header), ByteToInt32(payload));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Decode<T>(in ReadOnlySpan<byte> bytes, in string SecurityKey, in string SessionKey,
            out PacketHeader header) where T : class, IMessage<T>
        {
            var (headerLength, payloadLength) = DecodeLengths(bytes);

            header = PacketHeader.Parser.ParseFrom(bytes.Slice(HeaderOffset, headerLength).ToArray());

#if NET5_0_OR_GREATER
            ReadOnlySpan<byte> payload = header.EncryptionMode switch
            {
                PacketHeader.Types.EncryptionMode.KEncryptionServiceToken => Decrypt(bytes.Slice(HeaderOffset + headerLength, payloadLength), SecurityKey),
                PacketHeader.Types.EncryptionMode.KEncryptionSessionKey => Decrypt(bytes.Slice(HeaderOffset + headerLength, payloadLength), SessionKey),
                _ => bytes.Slice(HeaderOffset + headerLength, payloadLength),
            };
#elif NETSTANDARD2_0_OR_GREATER
            byte[] payload = null;
            switch (header.EncryptionMode)
            {
                case PacketHeader.Types.EncryptionMode.KEncryptionServiceToken:
                    Decrypt(bytes.Slice(HeaderOffset + headerLength, payloadLength), SecurityKey);
                    break;
                case PacketHeader.Types.EncryptionMode.KEncryptionSessionKey:
                    payload = Decrypt(bytes.Slice(HeaderOffset + headerLength, payloadLength), SessionKey);
                    break;
                case PacketHeader.Types.EncryptionMode.KEncryptionNone:
                default:
                    payload = bytes.Slice(HeaderOffset + headerLength, payloadLength).ToArray();
                    break;
            }
#endif
            //Log.Debug("Payload length: {0} | DecodedPayloadLen: {1}", payload.Length, header.DecodedPayloadLen);
            //Log.Debug("Payload Base64: {0}", Convert.ToBase64String(payload));
            //Dump(payload);

            Debug.Assert(payload != null, nameof(payload) + " != null");
            if (Convert.ToUInt32(payload.Length) != header.DecodedPayloadLen)
            {
                Log.Error("Payload length does not match");
                Log.Debug("Payload Data: {Data}", Convert.ToBase64String(payload));
                return null;
            }

            var obj = Parse<T>(payload);
            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object Decode(in Type type, in ReadOnlySpan<byte> bytes, in string securityKey,
            in string sessionKey, out PacketHeader header)
        {
            var (headerLength, payloadLength) = DecodeLengths(bytes);

            header = PacketHeader.Parser.ParseFrom(bytes.Slice(HeaderOffset, headerLength).ToArray());

#if NET5_0_OR_GREATER
            ReadOnlySpan<byte> payload = header.EncryptionMode switch
            {
                PacketHeader.Types.EncryptionMode.KEncryptionServiceToken => Decrypt(bytes.Slice(HeaderOffset + headerLength, payloadLength), securityKey),
                PacketHeader.Types.EncryptionMode.KEncryptionSessionKey => Decrypt(bytes.Slice(HeaderOffset + headerLength, payloadLength), sessionKey),
                _ => bytes.Slice(HeaderOffset + headerLength, payloadLength),
            };
#elif NETSTANDARD2_0_OR_GREATER
            byte[] payload;
            switch (header.EncryptionMode)
            {
                case PacketHeader.Types.EncryptionMode.KEncryptionServiceToken:
                    payload = Decrypt(bytes.Slice(HeaderOffset + headerLength, payloadLength), securityKey);
                    break;
                case PacketHeader.Types.EncryptionMode.KEncryptionSessionKey:
                    payload = Decrypt(bytes.Slice(HeaderOffset + headerLength, payloadLength), sessionKey);
                    break;
                case PacketHeader.Types.EncryptionMode.KEncryptionNone:
                default:
                    payload = bytes.Slice(HeaderOffset + headerLength, payloadLength).ToArray();
                    break;
            }
#endif

            if (Convert.ToUInt32(payload.Length) != header.DecodedPayloadLen)
            {
                Log.Error("Payload length does not match");
                Log.Debug("Payload Data: {Data}", Convert.ToBase64String(payload));
                return null;
            }

            var obj = Parse(type, payload);
            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] Encrypt(in string key, in ByteString body)
        {
#if NET5_0_OR_GREATER
            using var aes = Aes.Create();

            using var encryptor = aes.CreateEncryptor(Convert.FromBase64String(key), aes.IV);
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            body.WriteTo(cs);
            cs.FlushFinalBlock();

            var encrypted = ms.ToArray();

            return Encrypt(aes.IV, encrypted);
#elif NETSTANDARD2_0_OR_GREATER
            using (var aes = Aes.Create())
            {
                using (var encryptor = aes.CreateEncryptor(Convert.FromBase64String(key), aes.IV))
                {
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            body.WriteTo(cs);
                            cs.FlushFinalBlock();

                            var encrypted = ms.ToArray();

                            return Encrypt(aes.IV, encrypted);
                        }
                    }
                }
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] Encrypt(in string key, in ReadOnlySpan<byte> body)
        {
#if NET5_0_OR_GREATER
            using var aes = Aes.Create();

            using var encryptor = aes.CreateEncryptor(Convert.FromBase64String(key), aes.IV);
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            cs.Write(body);
            cs.FlushFinalBlock();

            var encrypted = ms.ToArray();

            return Encrypt(aes.IV, encrypted);
        }
#elif NETSTANDARD2_0_OR_GREATER
            using (var aes = Aes.Create())
            {
                using (var encryptor = aes.CreateEncryptor(Convert.FromBase64String(key), aes.IV))
                {
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(body.ToArray(), 0, body.Length);
                            cs.FlushFinalBlock();

                            var encrypted = ms.ToArray();

                            return Encrypt(aes.IV, encrypted);
                        }
                    }
                }
            }
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] Encrypt(in ReadOnlySpan<byte> iv, in ReadOnlySpan<byte> encrypted)
        {
            var len = iv.Length + encrypted.Length;

            Span<byte> payload = stackalloc byte[len];
            Copy(iv, 0, payload, 0, iv.Length);
            Copy(encrypted, 0, payload, iv.Length, encrypted.Length);

            return payload.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] Decrypt(in ReadOnlySpan<byte> bytes, in string key)
        {
#if NET5_0_OR_GREATER
            using var aes = Aes.Create();
            aes.Key = Convert.FromBase64String(key);
            aes.IV = bytes.Slice(0, IVLength).ToArray();
            using var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(bytes.Length - IVLength);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write);
            cs.Write(bytes.ToArray(), IVLength, bytes.Length - IVLength);
            cs.FlushFinalBlock();

            return ms.ToArray();
        }
#elif NETSTANDARD2_0_OR_GREATER
            using (var aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = bytes.Slice(0, IVLength).ToArray();
                using (var decryptor = aes.CreateDecryptor())
                {
                    using (var ms = new MemoryStream(bytes.Length - IVLength))
                    {
                        using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(bytes.ToArray(), IVLength, bytes.Length - IVLength);
                            cs.FlushFinalBlock();

                            return ms.ToArray();
                        }
                    }
                }
            }
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ByteString Compress(in ByteString payload) => GZip(CompressionMode.Compress, payload);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ByteString Decompress(in ByteString payload) => GZip(CompressionMode.Decompress, payload);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ByteString GZip(in CompressionMode mode, in ByteString payload)
        {
#if NET5_0_OR_GREATER
            using var input = new MemoryStream(payload.ToByteArray());
            using var gzip = new GZipStream(input, mode);
            using var output = new MemoryStream();

            gzip.CopyTo(output);

            output.Position = 0;

            return ByteString.FromStream(output);
#elif NETSTANDARD2_0_OR_GREATER
            using (var input = new MemoryStream(payload.ToByteArray()))
            {
                using (var gzip = new GZipStream(input, mode))
                {
                    using (var output = new MemoryStream())
                    {
                        gzip.CopyTo(output);

                        output.Position = 0;

                        return ByteString.FromStream(output);
                    }
                }
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static object Parse(in Type type, in object[] payload)
        {
            var pt = typeof(MessageParser<>).MakeGenericType(type);

            var parser = type.GetProperty("Parser", pt)?.GetValue(null);
            var method = pt.GetMethod("ParseFrom", new[] { typeof(ByteString) });

            var obj = method?.Invoke(parser, payload);
            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Parse<T>(in ReadOnlySpan<byte> payload) where T : class, IMessage<T> =>
            Parse(typeof(T), new object[] { ByteString.CopyFrom(payload) }) as T;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object Parse(in Type type, in ReadOnlySpan<byte> payload) =>
            Parse(type, new object[] { ByteString.CopyFrom(payload) });

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object Parse(in string typeName, in ByteString payload)
        {
            var type = Type.GetType($"AcFunDanmu.{typeName}");
            if (type != null)
            {
                return Parse(type, new object[] { payload });
            }
            else
            {
                Log.Warning("Unhandled type: {Type}", typeName);
                Log.Debug("Payload Data: {Data}", payload.ToBase64());
                return null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string Sign(in string uri, in string key, in Nonce nonce,
            in SortedList<string, string> extra = null)
        {
#if NET5_0_OR_GREATER
            using var hmac = new HMACSHA256(Convert.FromBase64String(key));
            string query =
 extra == null ? Query : string.Join('&', QueryDict.Concat(extra).OrderBy(query => query.Key).Select(query => $"{query.Key}={query.Value}"));

            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes($"POST&{uri}&{query}&{nonce.Result}"));

            Span<byte> sign = stackalloc byte[NonceSize + hash.Length];
#elif NETSTANDARD2_0_OR_GREATER
            byte[] hash;
            using (var hmac = new HMACSHA256(Convert.FromBase64String(key)))
            {
                var query = extra == null
                    ? Query
                    : string.Join("&",
                        QueryDict.Concat(extra).OrderBy(item => item.Key).Select(item => $"{item.Key}={item.Value}"));

                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes($"POST&{uri}&{query}&{nonce.Result}"));
            }

            var sign = new byte[NonceSize + hash.Length];
#endif
            if (BitConverter.IsLittleEndian)
            {
                sign[0] = nonce.b0;
                sign[1] = nonce.b1;
                sign[2] = nonce.b2;
                sign[3] = nonce.b3;
                sign[4] = nonce.b4;
                sign[5] = nonce.b5;
                sign[6] = nonce.b6;
                sign[7] = nonce.b7;
            }
            else
            {
                sign[0] = nonce.b7;
                sign[1] = nonce.b6;
                sign[2] = nonce.b5;
                sign[3] = nonce.b4;
                sign[4] = nonce.b3;
                sign[5] = nonce.b2;
                sign[6] = nonce.b1;
                sign[7] = nonce.b0;
            }

            for (var i = 0; i < hash.Length; i++)
            {
                sign[NonceSize + i] = hash[i];
            }

            return ToBase64Url(sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe string Sign(in string url, in string key, in long nonce,
            in SortedList<string, string> extra = null)
        {
            fixed (long* ptr = &nonce)
            {
                return Sign(url, key, Unsafe.AsRef<Nonce>(ptr), extra);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string Sign(in string url, in string key, in SortedList<string, string> extra = null) =>
            Sign(url, key, Random(), extra);

        private const int NonceSize = sizeof(long);

        [StructLayout(LayoutKind.Explicit, Pack = 4, Size = 8)]
        private struct Nonce
        {
            [FieldOffset(0)] public int Random;
            [FieldOffset(0)] public long Result;
            [FieldOffset(0)] public readonly byte b0;
            [FieldOffset(1)] public readonly byte b1;
            [FieldOffset(2)] public readonly byte b2;
            [FieldOffset(3)] public readonly byte b3;
            [FieldOffset(4)] public readonly byte b4;
            [FieldOffset(5)] public readonly byte b5;
            [FieldOffset(6)] public readonly byte b6;
            [FieldOffset(7)] public readonly byte b7;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Nonce Random()
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var nonce = new Nonce
            {
                Result = now
            };
#if NET5_0_OR_GREATER
            nonce.Random = RandomNumberGenerator.GetInt32(int.MaxValue);
#elif NETSTANDARD2_1_OR_GREATER
            nonce.Random = RandomNumberGenerator.GetInt32(int.MaxValue);
#elif NETSTANDARD2_0_OR_GREATER
            using (var generator = RandomNumberGenerator.Create())
            {
                var rand = new byte[4];
                generator.GetNonZeroBytes(rand);

                nonce.Random = BitConverter.ToInt32(rand, 0);
            }
#endif
            return nonce;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] FromBase64Url(in string text)
        {
            var temp = text.Replace('-', '+').Replace('_', '/');
            var rem = 4 - (temp.Length & 3);
            return Convert.FromBase64String(rem == 4 ? temp : temp.PadRight(temp.Length + rem, '='));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET5_0_OR_GREATER
        private static string ToBase64Url(in ReadOnlySpan<byte> data)
        {
            return Convert.ToBase64String(data).Replace('/', '_').Replace('+', '-').Trim('=');
        }
#elif NETSTANDARD2_0_OR_GREATER
        private static string ToBase64Url(in byte[] data)
        {
            return Convert.ToBase64String(data).Replace('/', '_').Replace('+', '-').Trim('=');
        }
#endif
    }
}