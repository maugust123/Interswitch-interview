using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Digests;

namespace InterviewApi.Common.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Checks for generic nullable objects
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="object">Object</param>
        /// <returns>True or False</returns>
        public static bool IsNull<T>(this T @object)
        {
            return Equals(@object, null);
        }

        /// <summary>
        /// Converts data to json
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T data)
        {
            return JsonConvert.SerializeObject(data);

        }

        public static HttpContent ToHttpContent<T>(this T model)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            return content;

        }

        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            string json = await content.ReadAsStringAsync();
            T value = JsonConvert.DeserializeObject<T>(json);
            return value;
        }

        /// <summary>
        /// Base64 encode
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToBase64Encode(this string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return text;
            }

            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }

        public static string ToSHA1(this string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            Sha1Digest sha1Digest = new Sha1Digest();
            sha1Digest.BlockUpdate(bytes, 0, bytes.Length);
            byte[] numArray = new byte[sha1Digest.GetDigestSize()];
            sha1Digest.DoFinal(numArray, 0);
            var signature = Convert.ToBase64String(numArray);
            return signature;
        }

        public static string ToSha1_2(this string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA1Managed sha1Managed = new SHA1Managed();
            byte[] hash = sha1Managed.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

    }
}
