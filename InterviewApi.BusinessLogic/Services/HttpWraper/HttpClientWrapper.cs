using System.Net.Http;
using InterviewApi.BusinessLogic.HttpBase;
using InterviewApi.Common;
using static InterviewApi.Common.Extensions.ObjectExtensions;
using Microsoft.Extensions.Options;

namespace InterviewApi.BusinessLogic.Services.HttpWraper
{
    public class HttpClientWrapper : BaseClient, IHttpClientWrapper
    {
        private readonly IInterSwitchAuth _interSwitchAuth;
        public HttpClientWrapper(IOptions<AppSecretsConfig> options, IInterSwitchAuth interSwitchAuth) : base(options)
        {
            _interSwitchAuth = interSwitchAuth;
        }
        public HttpResponseMessage Get(string uri)
        {
            var client = _interSwitchAuth.AddCustomHeaders(HttpClient, HttpMethod.Get.Method, uri);
            return client.GetAsync(uri).Result;
        }

        public HttpResponseMessage Post<T>(string uri, T model)
        {
            var client = _interSwitchAuth.AddCustomHeaders(HttpClient, HttpMethod.Post.Method, uri);

            return client.PostAsync(uri, model.ToHttpContent()).Result;
        }
    }
}
