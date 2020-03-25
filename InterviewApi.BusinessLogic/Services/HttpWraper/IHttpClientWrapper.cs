using System.Net.Http;
using InterviewApi.BusinessLogic.HttpBase;

namespace InterviewApi.BusinessLogic.Services.HttpWraper
{
    public interface IHttpClientWrapper : IBaseClient
    {
        HttpResponseMessage Get(string uri);
        HttpResponseMessage Post<T>(string uri, T model, string additionalParameters = "");
    }
}
