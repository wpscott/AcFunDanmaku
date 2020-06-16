using AcFunDanmuSongRequest.Platform.Interfaces;
using AcFunDanmuSongRequest.Platform.NetEase.Request;
using AcFunDanmuSongRequest.Platform.NetEase.Response;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AcFunDanmuSongRequest.Platform.NetEase
{
    class NetEasePlatform : BasePlatform
    {
        public NetEasePlatform(Config config) : base(config) { }
        public override async ValueTask<ISong> AddSong(string keyword)
        {
            //await Request<SuggestSearchResult>(new SuggestSearchGetRequest { Keyword = keyword, Offset = 0, Limit = 1 }, SearchResult.Options);
            //await Request<SearchGetResponse, EncodedResponse>(new SearchGetRequest { Keyword = keyword, Offset = 0, Limit = 1 }, SearchResult.Options);

            var result = await GetAsync<CloudSearchResponse, EncodedResponse>(new CloudSearchGetRequest { Keyword = keyword, Offset = 0, Limit = 1 }, SearchResult.Options);
            if (result.Songs.Length > 0)
            {
                var item = result.Songs[0];
                var song = new Song { Id = item.Id, Name = item.Name, Artist = item.Ar[0].Name, Album = item.Al.Name, Duration = item.Dt };
                Songs.Enqueue(song);
                return song;
            }
            else
            {
                return null;
            }
        }

        public override async ValueTask<ISong> NextSong()
        {
            if (Songs.Count > 0)
            {
                var song = (Song)Songs.Dequeue();
                //var source = await GetAsync<PlayResponse>(new PlayerV1GetRequest { Id = song.Id }, PlayResponse.Options);
                var source = await GetAsync<PlayResponse>(new PlayGetRequest { Id = song.Id, BitRate = 320000 }, PlayResponse.Options);
                song.Source = source.Data[0].Url;
                return song;
            }
            else
            {
                return null;
            }
        }

        private async Task GetLyric(long id)
        {
            await GetAsync<LyricResponse>(new LyricGetRequest { Id = id }, LyricResponse.Options);
        }


        internal static class NetEaseDecodeUtil
        {
            //private const string Key = "fuck~#$%^&*(458";
            private const int BlockSize = 64;
            private const int LengthOffset = 4;

            //private static readonly int[] DecodecKey = new int[] { 102, 117, 99, 107, 126, 35, 36, 37, 94, 38, 42, 40, 52, 53, 56 };
            private static readonly byte[] FullKey = new byte[] { 102, 117, 99, 107, 126, 35, 36, 37, 94, 38, 42, 40, 52, 53, 56, 102, 117, 99, 107, 126, 35, 36, 37, 94, 38, 42, 40, 52, 53, 56, 102, 117, 99, 107, 126, 35, 36, 37, 94, 38, 42, 40, 52, 53, 56, 102, 117, 99, 107, 126, 35, 36, 37, 94, 38, 42, 40, 52, 53, 56, 102, 117, 99, 107 };
            private static readonly int[] Code = new int[] { 82, 9, 106, -43, 48, 54, -91, 56, -65, 64, -93, -98, -127, -13, -41, -5, 124, -29, 57, -126, -101, 47, -1, -121, 52, -114, 67, 68, -60, -34, -23, -53, 84, 123, -108, 50, -90, -62, 35, 61, -18, 76, -107, 11, 66, -6, -61, 78, 8, 46, -95, 102, 40, -39, 36, -78, 118, 91, -94, 73, 109, -117, -47, 37, 114, -8, -10, 100, -122, 104, -104, 22, -44, -92, 92, -52, 93, 101, -74, -110, 108, 112, 72, 80, -3, -19, -71, -38, 94, 21, 70, 87, -89, -115, -99, -124, -112, -40, -85, 0, -116, -68, -45, 10, -9, -28, 88, 5, -72, -77, 69, 6, -48, 44, 30, -113, -54, 63, 15, 2, -63, -81, -67, 3, 1, 19, -118, 107, 58, -111, 17, 65, 79, 103, -36, -22, -105, -14, -49, -50, -16, -76, -26, 115, -106, -84, 116, 34, -25, -83, 53, -123, -30, -7, 55, -24, 28, 117, -33, 110, 71, -15, 26, 113, 29, 41, -59, -119, 111, -73, 98, 14, -86, 24, -66, 27, -4, 86, 62, 75, -58, -46, 121, 32, -102, -37, -64, -2, 120, -51, 90, -12, 31, -35, -88, 51, -120, 7, -57, 49, -79, 18, 16, 89, 39, -128, -20, 95, 96, 81, 127, -87, 25, -75, 74, 13, 45, -27, 122, -97, -109, -55, -100, -17, -96, -32, 59, 77, -82, 42, -11, -80, -56, -21, -69, 60, -125, 83, -103, 97, 23, 43, 4, 126, -70, 119, -42, 38, -31, 105, 20, 99, 85, 33, 12, 125 };
            //private static readonly char[] Char = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            #region Decode Key to byte array
#if DEBUG
            private static byte[] DecodeKey(string key)
            {
                var encoded = HttpUtility.UrlEncode(key).AsSpan();
                Span<byte> result = stackalloc byte[encoded.Length];
                int count = encoded.Length;
                for (var i = 0; i < encoded.Length; i++)
                {
                    if (encoded[i] == '%')
                    {
                        result[count] = byte.Parse(encoded.Slice(i + 1, 2), NumberStyles.HexNumber);
                        i += 2;
                        count++;
                    }
                    else
                    {
                        result[count] = (byte)encoded[i];
                        count++;
                    }
                }
                return result.Slice(0, count).ToArray();
            }
            private static byte[] ExpandKey(ReadOnlySpan<byte> key)
            {
                Span<byte> result = stackalloc byte[BlockSize];
                if (key == null || key.Length == 0) { result.Fill(0); }
                else if (key.Length >= BlockSize) { key.Slice(0, BlockSize).CopyTo(result); }
                else
                {
                    for (var index = 0; index < BlockSize; index++)
                    {
                        result[index] = key[index % key.Length];
                    }
                }
                return result.ToArray();
            }
#endif
            #endregion
            public static string Decode(string data)
            {
                if (string.IsNullOrEmpty(data)) { return null; }

                var dataLength = data.Length >> 1;
                if ((dataLength & 63) != 0) { return null; }

                ReadOnlySpan<char> dataSpan = data.AsSpan();
                Span<byte> decodedData = stackalloc byte[dataLength];
                for (var i = 0; i < dataLength; i++)
                {
                    decodedData[i] = byte.Parse(dataSpan.Slice(i << 1, 2), NumberStyles.HexNumber);
                }

                Span<byte> decryptedData = stackalloc byte[dataLength];
                for (var offset = 0; offset < dataLength; offset++)
                {
                    var key = offset < BlockSize ? FullKey[offset] : decodedData[offset - BlockSize];
                    decryptedData[offset] = (byte)((((byte)Code[(byte)Code[decodedData[offset]]] ^ key) - key) ^ FullKey[offset & 63]);
                }

                var lenData = decryptedData.Slice(dataLength - LengthOffset, LengthOffset);
                if (BitConverter.IsLittleEndian) { lenData.Reverse(); }
                var length = BitConverter.ToInt32(lenData);
                if (length > dataLength) { return null; }

                var hex = '%' + BitConverter.ToString(decryptedData.Slice(0, length).ToArray()).Replace('-', '%');
                return HttpUtility.UrlDecode(hex);
            }
        }

        internal static class NetEaseEncryptionUtil
        {
            private const string _IV = "0102030405060708";
            private static readonly byte[] IV = Encoding.UTF8.GetBytes(_IV);

            private const int MAX_ENCRYPT_BLOCK = 1024;
            private const string _Exponent = "010001";
            private static readonly BigInteger Exponent = BigInteger.Parse(_Exponent, NumberStyles.HexNumber);
            private const string _Modulus = "00e0b509f6259df8642dbc35662901477df22677ec152b5ff68ace615bb7b725152b3ab17a876aea8a5aa76d2e417629ec4ee341f56135fccf695280104e0312ecbda92557c93870114af6c9d05c4f7f0c3685b7a46bee255932575cce10b424d813cfe4875d3e82047b97ddef52741d546b8e289dc6935b3ece0462db0a22b8e7";
            private static readonly BigInteger Modulus = BigInteger.Parse(_Modulus, NumberStyles.HexNumber);
            private const string _AESKey = "0CoJUm6Qyw8W8jud";
            private static readonly byte[] AESKey = Encoding.UTF8.GetBytes(_AESKey);

            public static ReadOnlyDictionary<string, string> GenerateParams(string data)
            {
                var random = GenerateRandom();
                var p = EncodeParams(data, AESKey, random);
                var k = EncryptKey(Exponent, Modulus, random);

                return new ReadOnlyDictionary<string, string>(
                    new Dictionary<string, string> {
                        { "params",  p },
                        { "encSecKey", k }
                    }
                    );
            }

            private static string EncryptKey(BigInteger exponent, BigInteger modulus, ReadOnlySpan<byte> random)
            {
                var data = Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(random).Reverse().ToArray());

                var bData = new BigInteger(data.Reverse().Concat(new byte[] { 0 }).ToArray());

                var encrypted = BigInteger.ModPow(bData, exponent, modulus).ToByteArray().Reverse().ToArray();

                var hex = BitConverter.ToString(encrypted).Replace("-", string.Empty).ToLower();

                return hex;
            }

            private static string EncodeParams(string data, ReadOnlySpan<byte> aeskey, ReadOnlySpan<byte> random)
            {
                return EncodeParams(Encoding.UTF8.GetBytes(data), aeskey, random);
            }

            private static string EncodeParams(ReadOnlySpan<byte> data, ReadOnlySpan<byte> aeskey, ReadOnlySpan<byte> random)
            {
                var pass1 = AESEncrypt(aeskey, IV, data);

                var pass2 = AESEncrypt(random, IV, Encoding.UTF8.GetBytes(Convert.ToBase64String(pass1)));

                return Convert.ToBase64String(pass2);
            }

            private static byte[] AESEncrypt(ReadOnlySpan<byte> key, ReadOnlySpan<byte> iv, ReadOnlySpan<byte> text)
            {
                using var aes = Aes.Create();
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using var encryptor = aes.CreateEncryptor(key.ToArray(), iv.ToArray());
                using var ms = new MemoryStream();
                using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                cs.Write(text);
                cs.FlushFinalBlock();

                return ms.ToArray();
            }

            private const int RandomSize = 16;
            private static readonly char[] Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-_".ToCharArray();
            private static byte[] GenerateRandom()
            {
                Span<byte> data = stackalloc byte[RandomSize];
                Span<char> randomString = stackalloc char[RandomSize];
                using var crypto = new RNGCryptoServiceProvider();
                crypto.GetBytes(data);
                for (var i = 0; i < RandomSize; i++)
                {
                    randomString[i] = Chars[data[i] & 63];
                }
                return Encoding.UTF8.GetBytes(randomString.ToArray()).ToArray();

            }
        }
    }
}
