using System.Threading.Tasks;
using InterviewApi.BusinessLogic.Models;

namespace InterviewApi.BusinessLogic.Services.PaymentNotification
{
    public interface IPaymentNotificationService
    {
        Task<PaymentNotificationResponse> SendPaymentNotification(PaymentNotificationModel model);
    }
}
