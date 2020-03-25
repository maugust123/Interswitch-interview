using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using InterviewApi.Common.Extensions;
using Microsoft.Extensions.Logging;

namespace InterviewApi.BusinessLogic.HttpBase
{
    public class InterswitchAuth : IInterSwitchAuth
    {
        private readonly ILogger<InterswitchAuth> _logger;

        public InterswitchAuth(ILogger<InterswitchAuth> logger)
        {
            _logger = logger;
        }

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

        public Dictionary<string, string> GenerateInterSwitchAuth(string httpMethod, string resourceUrl, string additionalParameters)
        {
            var interSwitchAuth = new Dictionary<string, string>();

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

            if (!string.IsNullOrWhiteSpace(additionalParameters))
                signatureCipher = signatureCipher + "&" + additionalParameters;

            // encode signature as base 64 
            string signature = signatureCipher.ToSHA1(); //Approach 1
            string signature2 = signatureCipher.ToSha1_2(); //Approach 2

            interSwitchAuth.Add(Authorization, authorization);
            interSwitchAuth.Add(Timestamp, timestamp.ToString());
            interSwitchAuth.Add(Nonce, nonce);
            interSwitchAuth.Add(SignatureMethodKey, SignatureMethod);
            interSwitchAuth.Add(Signature, signature);
            interSwitchAuth.Add(TerminalIdKey, TerminalId);

            _logger.LogInformation($"Authentication details: {interSwitchAuth.ToJson()}");

            return interSwitchAuth;
        }

        public HttpClient AddCustomHeaders(HttpClient client, string httpMethod, string resourceUrl, string additionalParameters = "")
        {
            var url = $"{client.BaseAddress}{resourceUrl}";
            var dict = GenerateInterSwitchAuth(httpMethod, url, additionalParameters);

            foreach (var keyValuePair in dict)
                client.DefaultRequestHeaders.Add(keyValuePair.Key, keyValuePair.Value);

            return client;
        }

    }

    public interface IInterSwitchAuth
    {
        Dictionary<string, string> GenerateInterSwitchAuth(string httpMethod, string resourceUrl, string additionalParameters);
        HttpClient AddCustomHeaders(HttpClient client, string httpMethod, string resourceUrl, string additionalParameters = "");
    }
}
