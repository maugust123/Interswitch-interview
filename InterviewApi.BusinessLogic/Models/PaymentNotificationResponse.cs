using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewApi.BusinessLogic.Models
{
    public class PaymentNotificationResponse
    {
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }
        public string Customer { get; set; }
        public string RechargePIN { get; set; }
        public string TransferCode { get; set; }
        public string RequestReference { get; set; }
        public string TransactionRef { get; set; }
    }

}
