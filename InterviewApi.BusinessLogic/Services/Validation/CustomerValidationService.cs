using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using InterviewApi.BusinessLogic.Models;
using InterviewApi.BusinessLogic.Services.HttpWraper;
using InterviewApi.BusinessLogic.Services.Validation.ValidateCustomerModel;
using InterviewApi.Common.Exceptions;
using InterviewApi.Common.Extensions;
using static InterviewApi.Common.Settings;

namespace InterviewApi.BusinessLogic.Services.Validation
{
    public class CustomerValidationService : ICustomerValidationService
    {
        private readonly IHttpClientWrapper _httpClient;
        private readonly IValidateCustomerModelService _validateCustomerModel;

        public CustomerValidationService(IHttpClientWrapper httpClient, IValidateCustomerModelService validateCustomerModel)
        {
            _httpClient = httpClient;
            _validateCustomerModel = validateCustomerModel;
        }

        public async Task<ValidateCustomerResponse> ValidateCustomer(Models.ValidateCustomerModel model)
        {
            var errors = new List<string>();
            var validationResult = _validateCustomerModel.ValidateCustomer(model, errors);
            if (!validationResult)
            {
                throw new ApplicationValidationException(errors.ToJson());
            }

            HttpResponseMessage response = _httpClient.Post(ValidateUrl, model);
            return await response.Content.ReadAsJsonAsync<ValidateCustomerResponse>();
        }
    }
}
