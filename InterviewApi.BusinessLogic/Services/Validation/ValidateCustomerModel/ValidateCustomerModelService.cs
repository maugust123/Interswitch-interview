using System.Collections.Generic;
using System.Linq;
using InterviewApi.BusinessLogic.Validators;

namespace InterviewApi.BusinessLogic.Services.Validation.ValidateCustomerModel
{
    public class ValidateCustomerModelService : IValidateCustomerModelService
    {
        public bool ValidateCustomer(Models.ValidateCustomerModel model, List<string> errors)
        {
            var validator = new CustomerModelValidator();
            var result = validator.Validate(model);
            errors.AddRange(result.Errors.Select(error => error.ErrorMessage));
            return result.IsValid;
        }
    }
}
