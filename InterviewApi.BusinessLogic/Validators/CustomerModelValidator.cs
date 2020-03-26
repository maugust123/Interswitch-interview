using FluentValidation;
using static InterviewApi.Common.Settings;

namespace InterviewApi.BusinessLogic.Validators
{

    public class CustomerModelValidator : AbstractValidator<Models.ValidateCustomerModel>
    {
        public CustomerModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(customer => customer.TerminalId).NotEmpty().WithMessage(CanNotBeNull); ;
            RuleFor(customer => customer.BankCbnCode).NotNull().WithMessage(CanNotBeNull).NotEmpty().WithMessage(RequiredField);
            RuleFor(customer => customer.Amount).NotNull().WithMessage(CanNotBeNull).NotEmpty().WithMessage(RequiredField);
            RuleFor(customer => customer.PaymentCode).NotNull().WithMessage(CanNotBeNull).NotEmpty().WithMessage(RequiredField);
            RuleFor(customer => customer.CustomerMobile).NotNull().WithMessage(CanNotBeNull).NotEmpty().WithMessage(RequiredField);
            RuleFor(customer => customer.CustomerId).NotEmpty().WithMessage(CanNotBeNull);
            RuleFor(customer => customer.RequestReference).NotEmpty().WithMessage(CanNotBeNull);
            RuleFor(customer => customer.CustomerEmail).EmailAddress().When(p => !string.IsNullOrWhiteSpace(p.CustomerEmail)).WithMessage(InvalidValue);
        }

    }
}
