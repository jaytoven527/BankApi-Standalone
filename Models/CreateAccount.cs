namespace BankingApi_with_ReactFrontend.Server.Models
{
    public class CreateAccount
    {
        public string Type { get; set; }

        public string Owner { get; set; }

        public decimal InitialDeposit { get; set; } = 0;

    }
}
