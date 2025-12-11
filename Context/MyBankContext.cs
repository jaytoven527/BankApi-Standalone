using BankingApi_with_ReactFrontend.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingApi_with_ReactFrontend.Server.Context
{
    public class MyBankContext : DbContext
    {
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public MyBankContext(DbContextOptions<MyBankContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccount>()
                .Property(b => b.RowVersion)
                .IsRowVersion(); // marks it as a concurrency token
        }

    }
}
