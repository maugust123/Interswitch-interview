using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using InterviewApi.Common.Extensions;

namespace InterviewApi.BusinessLogic.HttpBase
{
    public class InterswitchAuth : IInterSwitchAuth
    {
        private const string ClientId = "IKIA794905AF56402FB3948B99E0F770AE8B8BFD284E";
        private const string ClientSecretKey = "ovbg/L/i8+eMrY41x0oz2O9XXpve1zWuzRoCV27jsIwaX+br9BPoMxzvDLV1E9Au";
        private const string Timestamp = "Timestamp";
        private const string Nonce = "Nonce";
        private const string SignatureMethod = "SHA1";
        private const string SignatureMethodKey = "SignatureMethod";
        private const string Signature = "Signature";
        private const string Authorization = "Authorization";
        private const string TerminalId = "3MCS0001";
        private const string TerminalIdKey = "TerminalId";
        private const string AuthorizationRealm = "InterswitchAuth";
        private const string Iso88591 = "ISO-8859-1";

        public Dictionary<string, string> GenerateInterSwitchAuth(string httpMethod, string resourceUrl)
        {
            var interSwitchAuth2 = new Dictionary<string, string>();

            //Timezone MUST be Africa/Lagos.
            var lagosTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Central Africa Standard Time");
            var calendar = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, lagosTimeZone);

            // Timestamp must be in seconds.
            long timestamp = calendar.Ticks / 1000;

            var nonce = Guid.NewGuid().ToString().Replace("-", "");

            var clientIdBase64 = ClientId.ToBase64Encode();
            var authorization = $"{AuthorizationRealm} {clientIdBase64}";

            var iso88591 = System.Text.Encoding.GetEncoding(Iso88591);

            var encodedResourceUrl = HttpUtility.UrlEncode(resourceUrl, iso88591);
            var signatureCipher = $"{httpMethod}&{encodedResourceUrl}&{timestamp}&{nonce}&{ClientId}&{ClientSecretKey}";
            
            // encode signature as base 64 
            string signature = signatureCipher.ToSHA512();


            interSwitchAuth2.Add(Authorization, authorization);
            interSwitchAuth2.Add(Timestamp, timestamp.ToString());
            interSwitchAuth2.Add(Nonce, nonce);
            interSwitchAuth2.Add(SignatureMethodKey, SignatureMethod);
            interSwitchAuth2.Add(Signature, signature);
            interSwitchAuth2.Add(TerminalIdKey, TerminalId);

            return interSwitchAuth2;
        }

        public HttpClient AddCustomHeaders(HttpClient client, string httpMethod, string resourceUrl)
        {
            var url = $"{client.BaseAddress}{resourceUrl}";
            var dict = GenerateInterSwitchAuth(httpMethod, url);

            //var auth = dict.FirstOrDefault(p => p.Key == Authorization).Value;
            //dict.Remove(AuthorizationRealm);

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Authorization, auth);

            foreach (var keyValuePair in dict)
                client.DefaultRequestHeaders.Add(keyValuePair.Key, keyValuePair.Value);

            return client;
        }

    }

    public interface IInterSwitchAuth
    {
        Dictionary<string, string> GenerateInterSwitchAuth(string httpMethod, string resourceUrl);
        HttpClient AddCustomHeaders(HttpClient client, string httpMethod, string resourceUrl);
    }
}
