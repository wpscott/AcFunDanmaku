﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using AcFunDanmu.Im.Basic;
using Google.Protobuf;
using Microsoft.Extensions.Logging;

namespace AcFunDanmu
{
    public static class ClientUtils
    {
        private const int HEADER_OFFSET = 12;
        private const int IV_LENGTH = 16;

        private static readonly SortedList<string, string> QueryDict = new SortedList<string, string>()
        {
            { "appver", "1.9.0.200" },
            { "sys", "PC_10" },
            { "kpn", "ACFUN_APP.LIVE_MATE" },
            { "kpf", "WINDOWS_PC" },
            { "subBiz", "mainApp" }
        };

        public static string Query => string.Join("&", QueryDict.Select(query => $"{query.Key}={query.Value}"));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Copy<T>(in ReadOnlySpan<T> source, in int sourceIndex, in Span<T> target,
            in int targetIndex, in int length)
        {
            for (var i = 0; i < length; i++) target[i + targetIndex] = source[i + sourceIndex];
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
        public static void EncodeAndSend(in NetworkStream tcpStream, in PacketHeader header, in UpstreamPayload body,
            in string key)
        {
            var (iv, encrypted) = Encrypt(key, body);

            var bHeader = header.ToByteArray();

            var len = HEADER_OFFSET + bHeader.Length + iv.Length + encrypted.Length;

            Span<byte> data = stackalloc byte[len];
            data[0] = 0xAB;
            data[1] = 0xCD;
            data[2] = 0x00;
            data[3] = 0x01;

            var (headerLength, payloadLength) = EncodeLengths(bHeader.Length, iv.Length + encrypted.Length);

            CopyLength(headerLength, data, 4);
            CopyLength(payloadLength, data, 8);
            Copy(bHeader, 0, data, HEADER_OFFSET, bHeader.Length);
            Copy(iv, 0, data, HEADER_OFFSET + bHeader.Length, iv.Length);
            Copy(encrypted, 0, data, HEADER_OFFSET + bHeader.Length + iv.Length, encrypted.Length);

            //Dump(data);

            //Decode<UpstreamPayload>(data, key, key, out _);

#if NET6_0_OR_GREATER
            tcpStream?.Write(data);
#elif NETSTANDARD2_0_OR_GREATER
            tcpStream?.Write(data.ToArray(), 0, len);
#endif
        }

        public static void Dump(in ReadOnlySpan<byte> data)
        {
            for (var i = 0; i < data.Length; i++)
            {
                Console.Write(data[i].ToString("X2"));
                if ((i & 1) == 1) Console.Write(" ");

                if ((i & 15) == 15) Console.WriteLine();
            }

            Console.WriteLine();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int ByteToInt32(in Length length)
        {
            return (length.b0 << 24) | (length.b1 << 16) | (length.b2 << 8) | (length.b3 << 0);
        }

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
        public static T Decode<T>(in MessageParser<T> parser, in ReadOnlySpan<byte> bytes, in string securityKey,
            in string sessionKey,
            out PacketHeader header) where T : class, IMessage<T>
        {
            var (headerLength, payloadLength) = DecodeLengths(bytes);

            header = PacketHeader.Parser.ParseFrom(bytes.Slice(HEADER_OFFSET, headerLength).ToArray());

#if NET6_0_OR_GREATER
            var payload = header.EncryptionMode switch
            {
                PacketHeader.Types.EncryptionMode.KEncryptionServiceToken => Decrypt(
                    bytes.Slice(HEADER_OFFSET + headerLength, payloadLength), securityKey, header.DecodedPayloadLen),
                PacketHeader.Types.EncryptionMode.KEncryptionSessionKey => Decrypt(
                    bytes.Slice(HEADER_OFFSET + headerLength, payloadLength), sessionKey, header.DecodedPayloadLen),
                _ => bytes.Slice(HEADER_OFFSET + headerLength, payloadLength)
            };
#elif NETSTANDARD2_0_OR_GREATER
            byte[] payload;
            switch (header.EncryptionMode)
            {
                case PacketHeader.Types.EncryptionMode.KEncryptionServiceToken:
                    payload = Decrypt(
                        bytes.Slice(HEADER_OFFSET + headerLength, payloadLength),
                        securityKey, header.DecodedPayloadLen);
                    break;
                case PacketHeader.Types.EncryptionMode.KEncryptionSessionKey:
                    payload = Decrypt(
                        bytes.Slice(HEADER_OFFSET + headerLength, payloadLength), sessionKey, header.DecodedPayloadLen);
                    break;
                case PacketHeader.Types.EncryptionMode.KEncryptionNone:
                default:
                    payload = bytes.Slice(HEADER_OFFSET + headerLength, payloadLength).ToArray();
                    break;
            }
#endif
            //Client.Logger.LogDebug("Payload length: {0} | DecodedPayloadLen: {1}", payload.Length, header.DecodedPayloadLen);
            //Client.Logger.LogDebug("Payload Base64: {0}", Convert.ToBase64String(payload));
            //Dump(payload);

            Debug.Assert(payload != null, nameof(payload) + " != null");
            if (Convert.ToUInt32(payload.Length) != header.DecodedPayloadLen)
            {
                Client.Logger.LogError("Payload length does not match");
                Client.Logger.LogDebug("Payload Data: {Data}", Convert.ToBase64String(payload));
                return null;
            }

            return parser.ParseFrom(payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (byte[], byte[]) Encrypt(in string key, in UpstreamPayload body)
        {
#if NET6_0_OR_GREATER
            using var aes = Aes.Create();

            using var encryptor = aes.CreateEncryptor(Convert.FromBase64String(key), aes.IV);
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            body.WriteTo(cs);
            cs.FlushFinalBlock();

            return (aes.IV, ms.ToArray());
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
                            body.WriteTo(cs);
                            cs.FlushFinalBlock();

                            return (aes.IV, ms.ToArray());
                        }
                    }
                }
            }
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] Decrypt(in ReadOnlySpan<byte> bytes, in string key, in uint decryptedLength)
        {
#if NET6_0_OR_GREATER
            using var aes = Aes.Create();
            aes.Key = Convert.FromBase64String(key);
            aes.IV = bytes[..IV_LENGTH].ToArray();
            using var decryptor = aes.CreateDecryptor();
            return decryptor.TransformFinalBlock(bytes.ToArray(), IV_LENGTH, bytes.Length - IV_LENGTH);
#elif NETSTANDARD2_0_OR_GREATER
            using (var aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = bytes.Slice(0, IV_LENGTH).ToArray();
                using (var decryptor = aes.CreateDecryptor())
                {
                    return decryptor.TransformFinalBlock(bytes.ToArray(), IV_LENGTH, bytes.Length - IV_LENGTH);
                }
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ByteString Compress(in ByteString payload)
        {
            return GZip(CompressionMode.Compress, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ByteString Decompress(in ByteString payload)
        {
            return GZip(CompressionMode.Decompress, payload);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ByteString GZip(in CompressionMode mode, in ByteString payload)
        {
#if NET6_0_OR_GREATER
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

        private const int HMAC_SHA256_SIZE = 32;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe string Sign(in string uri, in string key, in Nonce nonce,
            in IEnumerable<KeyValuePair<string, string>> extra = null)
        {
#if NET6_0_OR_GREATER
            using var hmac = new HMACSHA256(Convert.FromBase64String(key));
            var query =
                extra == null
                    ? Query
                    : string.Join('&',
                        QueryDict.Concat(extra).OrderBy(query => query.Key)
                            .Select(query => $"{query.Key}={query.Value}"));

            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes($"POST&{uri}&{query}&{nonce.Result}"));

            Span<byte> sign = stackalloc byte[NONCE_SIZE + hash.Length];
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

            var sign = new byte[NONCE_SIZE + hash.Length];
#endif
            fixed (byte* pData = nonce.Data)
            {
                if (BitConverter.IsLittleEndian)
                {
                    sign[0] = *(pData + 7);
                    sign[1] = *(pData + 6);
                    sign[2] = *(pData + 5);
                    sign[3] = *(pData + 4);
                    sign[4] = *(pData + 3);
                    sign[5] = *(pData + 2);
                    sign[6] = *(pData + 1);
                    sign[7] = *(pData + 0);
                }
                else
                {
                    sign[0] = *(pData + 0);
                    sign[1] = *(pData + 1);
                    sign[2] = *(pData + 2);
                    sign[3] = *(pData + 3);
                    sign[4] = *(pData + 4);
                    sign[5] = *(pData + 5);
                    sign[6] = *(pData + 6);
                    sign[7] = *(pData + 7);
                }
            }

            for (var i = 0; i < HMAC_SHA256_SIZE; i += 8)
            {
                sign[NONCE_SIZE + i + 0] = hash[i + 0];
                sign[NONCE_SIZE + i + 1] = hash[i + 1];
                sign[NONCE_SIZE + i + 2] = hash[i + 2];
                sign[NONCE_SIZE + i + 3] = hash[i + 3];
                sign[NONCE_SIZE + i + 4] = hash[i + 4];
                sign[NONCE_SIZE + i + 5] = hash[i + 5];
                sign[NONCE_SIZE + i + 6] = hash[i + 6];
                sign[NONCE_SIZE + i + 7] = hash[i + 7];
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
        public static string Sign(in string url, in string key,
            in IEnumerable<KeyValuePair<string, string>> extra = null)
        {
            return Sign(url, key, Random(), extra);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe Nonce Random()
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / 60;

#if NET6_0_OR_GREATER
            var bytes = RandomNumberGenerator.GetBytes(4);
            fixed (byte* pByte = bytes)
            {
                var nonce = new Nonce
                {
                    Result = now
                };
                nonce.Random[0] = *(pByte + 0);
                nonce.Random[1] = *(pByte + 1);
                nonce.Random[2] = *(pByte + 2);
                nonce.Random[3] = *(pByte + 3);
                return nonce;
            }
#elif NETSTANDARD2_0_OR_GREATER
            using (var generator = RandomNumberGenerator.Create())
            {
                var bytes = new byte[4];
                generator.GetNonZeroBytes(bytes);

                fixed (byte* pByte = bytes)
                {
                    var nonce = new Nonce
                    {
                        Result = now
                    };
                    nonce.Random[0] = *(pByte + 0);
                    nonce.Random[1] = *(pByte + 1);
                    nonce.Random[2] = *(pByte + 2);
                    nonce.Random[3] = *(pByte + 3);
                    return nonce;
                }
            }
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] FromBase64Url(in string text)
        {
            var temp = text.Replace('-', '+').Replace('_', '/');
            var rem = 4 - (temp.Length & 3);
            return Convert.FromBase64String(rem == 4 ? temp : temp.PadRight(temp.Length + rem, '='));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#if NET6_0_OR_GREATER
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

        [StructLayout(LayoutKind.Explicit, Pack = 4, Size = 4)]
        private readonly struct Length
        {
            [FieldOffset(0)] public readonly byte b0;
            [FieldOffset(1)] public readonly byte b1;
            [FieldOffset(2)] public readonly byte b2;
            [FieldOffset(3)] public readonly byte b3;
        }

        private const int NONCE_SIZE = sizeof(long);

        [StructLayout(LayoutKind.Explicit, Pack = NONCE_SIZE, Size = NONCE_SIZE)]
        private unsafe struct Nonce
        {
            [FieldOffset(0)] public long Result;
            [FieldOffset(4)] public fixed byte Random[NONCE_SIZE >> 1];
            [FieldOffset(0)] public fixed byte Data[NONCE_SIZE];
        }

        public const string KuaishouZt = "https://api.kuaishouzt.com";
        public const string AuthorAuth = "/rest/zt/live/authorAuth";
        public const string LiveConfig = "/rest/zt/live/web/obs/config";
        public const string LiveStatus = "/rest/zt/live/web/obs/status";
        public const string StartPush = "/rest/zt/live/startPush";
        public const string StopPush = "/rest/zt/live/stopPush";
        public const string GiftAll = "/rest/zt/live/gift/all";
    }
}