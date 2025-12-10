namespace BankingApi_with_ReactFrontend.Server.Models
{
    public class UpdateAccountFromTransaction
    {
        public string Type { get; set; }

        public decimal Amount { get; set; }

        public Guid BankAccountId { get; set; }


    }
}
