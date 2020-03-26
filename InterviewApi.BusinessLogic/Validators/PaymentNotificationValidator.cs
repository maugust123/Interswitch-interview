using FluentValidation;
using InterviewApi.BusinessLogic.Models;
using static InterviewApi.Common.Settings;

namespace InterviewApi.BusinessLogic.Validators
{
    public class PaymentNotificationValidator : AbstractValidator<PaymentNotificationModel>
    {
        public PaymentNotificationValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(payment => payment.TerminalId).NotEmpty().WithMessage(CanNotBeNull); ;
            RuleFor(payment => payment.BankCbnCode).NotNull().WithMessage(CanNotBeNull).NotEmpty().WithMessage(RequiredField);
            RuleFor(payment => payment.Amount).NotNull().WithMessage(CanNotBeNull).NotEmpty().WithMessage(RequiredField);
            RuleFor(payment => payment.PaymentCode).NotNull().WithMessage(CanNotBeNull).NotEmpty().WithMessage(RequiredField);
            RuleFor(payment => payment.CustomerMobile).NotNull().WithMessage(CanNotBeNull).NotEmpty().WithMessage(RequiredField);
            RuleFor(payment => payment.CustomerId).NotEmpty().WithMessage(CanNotBeNull);
            RuleFor(payment => payment.RequestReference).NotEmpty().WithMessage(CanNotBeNull);
            RuleFor(payment => payment.CustomerEmail).EmailAddress().When(p => !string.IsNullOrWhiteSpace(p.CustomerEmail)).WithMessage(InvalidValue);
        }

    }

}
