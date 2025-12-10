using BankingApi_with_ReactFrontend.Server.Context;
using BankingApi_with_ReactFrontend.Server.Entities;
using BankingApi_with_ReactFrontend.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BankingApi_with_ReactFrontend.Server.Services
{
    public class ProcessTransaction
    {
        private readonly MyBankContext _myBankContext;

        public ProcessTransaction(MyBankContext MyBankContext)
        {
            _myBankContext = MyBankContext;
        }
        public async Task<Transaction> AsDeposit(UpdateAccountFromTransaction NewTransaction)
        {
            var id = NewTransaction.BankAccountId;

            var BankAcct = await _myBankContext.BankAccounts.FindAsync(id);
            if (BankAcct == null) throw new Exception();


            Transaction NewDeposit = new Transaction
            {
                Type = NewTransaction.Type,
                Amount = NewTransaction.Amount,
                BankAccountId = id,
            };

            BankAcct.Balance += NewDeposit.Amount;

            await _myBankContext.Transactions.AddAsync(NewDeposit);
            await _myBankContext.SaveChangesAsync();

            return NewDeposit;


        }

        public async Task<Transaction> AsWithdraw(UpdateAccountFromTransaction NewTransaction)
        {
            var id = NewTransaction.BankAccountId;

            var BankAcct = await _myBankContext.BankAccounts.FindAsync(id);
            if(BankAcct == null) throw new Exception();
            if (BankAcct.Balance < NewTransaction.Amount) throw new Exception();

            Transaction NewWithdrawal = new Transaction
            {
                Type = NewTransaction.Type,
                Amount = NewTransaction.Amount,
                BankAccountId = id,
            };

            BankAcct.Balance -= NewWithdrawal.Amount;

            await _myBankContext.Transactions.AddAsync(NewWithdrawal);
            await _myBankContext.SaveChangesAsync();


            return NewWithdrawal;

        }
    }
}
