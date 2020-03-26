using System;
using System.Collections.Generic;
using System.Text;
using InterviewApi.BusinessLogic.Models;

namespace InterviewApi.BusinessLogic.Services.PaymentNotification.Validation
{
    public interface IPaymentValidationService
    {
        bool ValidatePaymentNotification(PaymentNotificationModel model, List<string> errors);
    }
}
