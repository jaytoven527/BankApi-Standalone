namespace BankingApi_with_ReactFrontend.Server.Models
{
    public class GetAccountModel
    {
        public string Type { get; set; }

        public string Owner { get; set; }

        public DateTime OpenedOn { get; set; }
    }
}
