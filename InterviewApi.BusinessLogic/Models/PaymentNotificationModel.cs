using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewApi.BusinessLogic.Models
{
    public class PaymentNotificationModel
    {
        public double Amount { get; set; }
        public int Surcharge { get; set; }
        public string TerminalId { get; set; }
        public string RequestReference { get; set; }
        public string CustomerId { get; set; }
        public int BankCbnCode { get; set; }
        public int CustomerMobile { get; set; }
        public string TransactionRef { get; set; }
        public string CustomerEmail { get; set; }
        public int PaymentCode { get; set; }
    }

}
