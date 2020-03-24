namespace InterviewApi.BusinessLogic.Models
{
    public class ValidateCustomerResponse
    {
        public string TransactionRef { get; set; }
        public string Biller { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PaymentItem { get; set; }
        public string Narration { get; set; }
        public double Amount { get; set; }
        public bool IsAmountFixed { get; set; }
        public int CollectionsAccountNumber { get; set; }
        public int CollectionsAccountType { get; set; }
        public int Surcharge { get; set; }
        public int Excise { get; set; }
        public bool SurchargeType { get; set; }
        public int PaymentItemId { get; set; }
        public int Balance { get; set; }
        public string BalanceNarration { get; set; }
        public int ResponseCode { get; set; }
        public bool DisplayBalance { get; set; }
        public string BalanceType { get; set; }
        public string ShortTransactionRef { get; set; }
    }

}
