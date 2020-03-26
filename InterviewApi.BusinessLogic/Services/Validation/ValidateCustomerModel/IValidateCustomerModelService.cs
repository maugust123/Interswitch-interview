using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewApi.BusinessLogic.Services.Validation.ValidateCustomerModel
{
    public interface IValidateCustomerModelService
    {
        bool ValidateCustomer(Models.ValidateCustomerModel model, List<string> errors);
    }
}
