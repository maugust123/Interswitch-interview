using System.Threading.Tasks;
using InterviewApi.BusinessLogic.Models;

namespace InterviewApi.BusinessLogic.Services.Validation
{
    public interface ICustomerValidationService
    {
        Task<ValidateCustomerResponse> ValidateCustomer(ValidateCustomerModel model);
    }
}
