namespace BankingApi_with_ReactFrontend.Server.Models
{
    public class TransactionLineItem
    {
        public TransactionType TransactionType { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public string BankAccountType { get; set; }

        public Guid BankAccountId { get; set; }

        public Guid TransactionId { get; set; }
    }
}
