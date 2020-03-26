using System.Collections.Generic;
using System.Linq;
using InterviewApi.BusinessLogic.Models;
using InterviewApi.BusinessLogic.Validators;

namespace InterviewApi.BusinessLogic.Services.PaymentNotification.Validation
{
    public class PaymentValidationService : IPaymentValidationService
    {
        public bool ValidatePaymentNotification(PaymentNotificationModel model, List<string> errors)
        {
            var validator = new PaymentNotificationValidator();
            var result = validator.Validate(model);
            errors.AddRange(result.Errors.Select(error => error.ErrorMessage));
            return result.IsValid;
        }
    }
}
