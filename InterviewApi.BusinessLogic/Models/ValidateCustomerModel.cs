namespace InterviewApi.BusinessLogic.Models
{
    public class ValidateCustomerModel
    {
        public string RequestReference { get; set; }
        public string CustomerId { get; set; }
        public int BankCbnCode { get; set; }
        public double Amount { get; set; }
        public int CustomerMobile { get; set; }
        public string TerminalId { get; set; }
        public string CustomerEmail { get; set; }
        public int PaymentCode { get; set; }
    }



}
