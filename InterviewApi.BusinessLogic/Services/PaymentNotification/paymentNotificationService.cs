﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using InterviewApi.BusinessLogic.Models;
using InterviewApi.BusinessLogic.Services.HttpWraper;
using InterviewApi.Common.Extensions;
using static InterviewApi.Common.Settings;

namespace InterviewApi.BusinessLogic.Services.PaymentNotification
{
    public class PaymentNotificationService: IPaymentNotificationService
    {
        private readonly IHttpClientWrapper _httpClient;

        public PaymentNotificationService(IHttpClientWrapper httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaymentNotificationResponse> SendPaymentNotification(PaymentNotificationModel model)
        {
            HttpResponseMessage response = _httpClient.Post(PaymentNotificationUrl, model);
            return await response.Content.ReadAsJsonAsync<PaymentNotificationResponse>();
        }
    }
}
