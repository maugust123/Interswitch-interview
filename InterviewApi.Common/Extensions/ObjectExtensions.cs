using Newtonsoft.Json;
using System;
using System.Linq;
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
        /// <param name="@object">Object</param>
        /// <returns>True or False</returns>
        public static bool IsNull<T>(this T @object)
        {
            return Equals(@object, null);
        }

        /// <summary>
        /// Checks if the object is not null
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="@object">Object</param>
        /// <returns>True or false</returns>
        public static bool IsNotNull<T>(this T @object)
        {
            return !IsNull(@object);
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

        /// <summary>
        /// Deserialize string to Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(this string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            string json = await content.ReadAsStringAsync();
            T value = JsonConvert.DeserializeObject<T>(json);
            return value;
        }

        public static string ToBase64Encode(this string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return text;
            }

            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }

        public static string ToBase64Decode(this string base64EncodedText)
        {
            if (String.IsNullOrEmpty(base64EncodedText))
            {
                return base64EncodedText;
            }

            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedText);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }


        public static string ToSHA512(this string text)
        {
            text = "hell";

            byte[] bytes = Encoding.UTF8.GetBytes(text);
            Sha1Digest sha1Digest = new Sha1Digest();
            sha1Digest.BlockUpdate(bytes, 0, bytes.Length);
            byte[] numArray = new byte[sha1Digest.GetDigestSize()];
            sha1Digest.DoFinal(numArray, 0);
            var signature= Convert.ToBase64String(numArray);
            return signature;
        }

        public static byte[] Hash(byte[] data)
        {
            Sha256Digest sha256 = new Sha256Digest();
            sha256.BlockUpdate(data, 0, data.Length);
            byte[] hash = new byte[sha256.GetDigestSize()];
            sha256.DoFinal(hash, 0);
            return hash;
        }

        public static string ComputeHash(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            Sha1Digest sha1Digest = new Sha1Digest();
            sha1Digest.BlockUpdate(bytes, 0, bytes.Length);
            byte[] numArray = new byte[sha1Digest.GetDigestSize()];
            sha1Digest.DoFinal(numArray, 0);
            return Convert.ToBase64String(numArray);
        }

    }
}
