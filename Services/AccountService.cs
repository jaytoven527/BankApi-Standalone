using BankingApi_with_ReactFrontend.Server.Context;
using BankingApi_with_ReactFrontend.Server.Entities;
using BankingApi_with_ReactFrontend.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Diagnostics.Metrics;

namespace BankingApi_with_ReactFrontend.Server.Services
{
    public class AccountService : IAccountService
    {
        private readonly MyBankContext _myBankContext;

        public AccountService(MyBankContext MyBankContext)
        {
            _myBankContext = MyBankContext;
        }

        public async Task<Guid> CreateAccountAsync(CreateAccount NewAccount)
        {   using var dbTransaction = await _myBankContext.Database.BeginTransactionAsync();
            
            try
            {
                BankAccount NewBankAccount = new BankAccount
                {
                    Type = NewAccount.Type,
                    Owner = NewAccount.Owner,
                    Balance = NewAccount.InitialDeposit
                };

                await _myBankContext.BankAccounts.AddAsync(NewBankAccount);
                await _myBankContext.SaveChangesAsync();

                if (NewBankAccount.Balance > 0)
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
                
                await dbTransaction.CommitAsync();
                
                return NewBankAccount.Id;
            }
            catch (DbUpdateConcurrencyException)
            {
                await dbTransaction.RollbackAsync();
                throw new InvalidOperationException("There was a concurrency issue.");
            }  
            catch
            {
                await dbTransaction.RollbackAsync();
                throw new InvalidOperationException("Something went wrong here.");
            }
           

            
        }

        public async Task<Transaction> DoTransactionAsync(UpdateAccountFromTransaction NewTransaction)
        {   using var dbTransaction = await _myBankContext.Database.BeginTransactionAsync();
                       
            try
            {
                var BankAcct = await _myBankContext.BankAccounts.FindAsync(NewTransaction.BankAccountId);
                if (BankAcct == null) throw new InvalidOperationException("Account could not be found.");

                string transcationType = NewTransaction.Type.Trim().ToLower();

                switch (transcationType)
                {
                    case "deposit":
                        if (NewTransaction.Amount <= 0)
                            throw new InvalidOperationException("Deposit amount must be positive. Please input a dollar amount greater than 0.");
                        BankAcct.Balance += NewTransaction.Amount;

                        break;

                    case "withdraw":
                        if (NewTransaction.Amount <= 0)
                            throw new InvalidOperationException("Withdrawal amount must be positive. Please input a dollar amount greater than 0.");
                        if (BankAcct.Balance < NewTransaction.Amount)
                            throw new InvalidOperationException("Insufficient funds.");
                        BankAcct.Balance -= NewTransaction.Amount;

                        break;

                    default:
                        throw new InvalidOperationException("Unsupported Transaction Operation.");
                }

                Transaction Record = new Transaction
                {
                    Type = NewTransaction.Type,
                    Amount = NewTransaction.Amount,
                    BankAccountId = NewTransaction.BankAccountId,
                };

                await _myBankContext.Transactions.AddAsync(Record);
                await _myBankContext.SaveChangesAsync();               

                await dbTransaction.CommitAsync();

                return Record;
            }
            catch (DbUpdateConcurrencyException)
            {
                await dbTransaction.RollbackAsync();
                throw new InvalidOperationException("There was a concurrency issue.");                
            } 
            catch
            {
                await dbTransaction.RollbackAsync();
                throw;
            }


        }

    }
}
