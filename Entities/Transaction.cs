using BankingApi_with_ReactFrontend.Server.Helper;

namespace BankingApi_with_ReactFrontend.Server.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; } = SequentialGuid.NewSequentialGuid();

        public string Type { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public Guid BankAccountId { get; set; }

        public BankAccount BankAccount { get; set; } = null!;
    }
}
