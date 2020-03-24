using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using InterviewApi.Common;
using Microsoft.Extensions.Options;

namespace InterviewApi.BusinessLogic.HttpBase
{
    public class BaseClient : ReadConfig, IBaseClient
    {
        public string Resource { get; set; }
        private const string AuthorizationRealm = "InterswitchAuth";
        public HttpClient HttpClient { get; private set; }
        public BaseClient(IOptions<AppSecretsConfig> options) : base(options)
        {
            HttpClient = new HttpClient { BaseAddress = new Uri(BaseUrl), DefaultRequestVersion = new Version(2, 0) };
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

    }


    public interface IBaseClient
    {
    }
}
