namespace InterviewApi.Common
{
    public class Settings
    {
        public const string PaymentNotificationUrl = "api/v1A/svapayments/sendAdviceRequest";
        public const string ValidateUrl = "api/v1A/svapayments/validateCustomer";

        public const string InvalidValue = "{PropertyValue} is invalid for {PropertyName} field";
        public const string CanNotBeNull = "{PropertyName} can not be {PropertyValue}";
        public const string RequiredField = "{PropertyName} is required";

    }
}
