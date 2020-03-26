using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using InterviewApi.BusinessLogic.Models;
using InterviewApi.BusinessLogic.Services.HttpWraper;
using InterviewApi.BusinessLogic.Services.PaymentNotification.Validation;
using InterviewApi.Common.Exceptions;
using InterviewApi.Common.Extensions;
using static InterviewApi.Common.Settings;

namespace InterviewApi.BusinessLogic.Services.PaymentNotification
{
    public class PaymentNotificationService : IPaymentNotificationService
    {
        private readonly IHttpClientWrapper _httpClient;
        private readonly IPaymentValidationService _paymentValidation;

        public PaymentNotificationService(IHttpClientWrapper httpClient, IPaymentValidationService paymentValidation)
        {
            _httpClient = httpClient;
            _paymentValidation = paymentValidation;
        }

        public async Task<PaymentNotificationResponse> SendPaymentNotification(PaymentNotificationModel model)
        {
            var errors = new List<string>();
            var validationResult = _paymentValidation.ValidatePaymentNotification(model, errors);
            if (!validationResult)
            {
                throw new ApplicationValidationException(errors.ToJson());
            }

            var additionalParams = $"&{model.Amount}&{model.RequestReference}&{model.CustomerId}&{model.PaymentCode}";
            HttpResponseMessage response = _httpClient.Post(PaymentNotificationUrl, model, additionalParams);
            return await response.Content.ReadAsJsonAsync<PaymentNotificationResponse>();
        }
    }
}
