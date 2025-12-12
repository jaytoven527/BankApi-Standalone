namespace BankingApi_with_ReactFrontend.Server.Models
{
    public class UpdateAccountFromTransaction
    {
        public TransactionType Type { get; set; }

        public decimal Amount { get; set; }

        public Guid BankAccountId { get; set; }


    }
}
