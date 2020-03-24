using System.Net.Http;
using System.Threading.Tasks;
using InterviewApi.BusinessLogic.Models;
using InterviewApi.BusinessLogic.Services.HttpWraper;
using InterviewApi.Common.Extensions;
using static InterviewApi.Common.Settings;

namespace InterviewApi.BusinessLogic.Services.Validation
{
    public class CustomerValidationService : ICustomerValidationService
    {
        private readonly IHttpClientWrapper _httpClient;

        public CustomerValidationService(IHttpClientWrapper httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ValidateCustomerResponse> ValidateCustomer(ValidateCustomerModel model)
        {
            HttpResponseMessage response = _httpClient.Post(ValidateUrl, model);
            return await response.Content.ReadAsJsonAsync<ValidateCustomerResponse>();
        }
    }
}
