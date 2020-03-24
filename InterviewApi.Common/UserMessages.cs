using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewApi.Common
{
    public static class UserMessages
    {
        
        public const string GreaterThan = "{PropertyName} must be greater than {ComparisonValue}";
        public const string GreaterThanOrEqualTo = "{PropertyName} must be greater than or equal to {ComparisonValue}";
        public const string LessThanOrEqualTo = "{PropertyName} must be less than or equal to {ComparisonValue}";
        public const string NotFound = "{0} with the specified Id is not found";
        public const string PhoneNumberRegex = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
        public const string WebsiteRegex = @"^(http|https):\/\/[\w\d]+\.[\w]+(\/[\w\d]+)$";
        public const string InvalidValue = "{PropertyValue} is invalid for {PropertyName} field";
        public const string CanNotBeNull = "{PropertyName} can not be {PropertyValue}";
        public const string EqualToStartDate = "{PropertyName} must be equal to the start date of the month";
        public const string Required = "{PropertyName} is required";
        public const string InvalidField = "{PropertyName} is invalid";
        public const string HomeCurrency = "The home currency is already defined";
        public const string EmptyEmailAddress = "Email address can not be empty";
        public const string EnterNumberMsg = "Enter new numbers:";
        public const string ExistMessage = "Type exit to close the program";
        public const string ExitProgramMsg = "exit";
    }
}
