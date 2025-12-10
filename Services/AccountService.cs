using BankingApi_with_ReactFrontend.Server.Context;
using BankingApi_with_ReactFrontend.Server.Entities;
using BankingApi_with_ReactFrontend.Server.Models;
using System.Diagnostics.Metrics;

namespace BankingApi_with_ReactFrontend.Server.Services
{
    public class AccountService : IAccountService
    {
        private readonly MyBankContext _myBankContext;
        private readonly ProcessTransaction _processTransaction;

        public AccountService(MyBankContext MyBankContext, ProcessTransaction ProcessTransaction)
        {
            _myBankContext = MyBankContext;
            _processTransaction = ProcessTransaction;
        }

        public async Task<Guid> CreateAccountAsync(CreateAccount NewAccount)
        {
            BankAccount NewBankAccount = new BankAccount
            {
                Type = NewAccount.Type,
                Owner = NewAccount.Owner,
                Balance = NewAccount.InitialDeposit
            };

            await _myBankContext.BankAccounts.AddAsync(NewBankAccount);
            await _myBankContext.SaveChangesAsync();

            if(NewBankAccount.Balance > 0)
            {
                Transaction InitialDeposit = new Transaction
                {
                    Type = "Deposit",
                    Amount = NewBankAccount.Balance,
                    BankAccountId = NewBankAccount.Id
                };


                await _myBankContext.Transactions.AddAsync(InitialDeposit);
                await _myBankContext.SaveChangesAsync();
            }

            return NewBankAccount.Id;
        }

        public async Task<Transaction> DoTransactionAsync(UpdateAccountFromTransaction NewTransaction)
        {
            string TranscationType = NewTransaction.Type.Trim().ToLower();
            Transaction TransactionRecord;

            switch (TranscationType)
            {
                case "deposit":
                    TransactionRecord = await _processTransaction.AsDeposit(NewTransaction);
                    return TransactionRecord;

                case "withdraw":
                    TransactionRecord = await _processTransaction.AsWithdraw(NewTransaction);
                    return TransactionRecord;

                default:
                    throw new InvalidOperationException();                  

            }


        }

    }
}
