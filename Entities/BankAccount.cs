using BankingApi_with_ReactFrontend.Server.Helper;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace BankingApi_with_ReactFrontend.Server.Entities
{
    public class BankAccount
    {
        public Guid Id { get; set; } = SequentialGuid.NewSequentialGuid();
        
        public string Owner { get; set; }

        public decimal Balance { get; set; }

        public string Type { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public ICollection<Transaction> Transactions { get; } = new List<Transaction>();

        [JsonIgnore]
        public byte[]? RowVersion { get; set; }

    }
}
