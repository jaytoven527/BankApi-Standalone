using BankingApi_with_ReactFrontend.Server.Entities;
using BankingApi_with_ReactFrontend.Server.Models;

namespace BankingApi_with_ReactFrontend.Server.Services
{
    public interface IAccountService
    {
        Task<Guid> CreateAccountAsync(CreateAccount NewAccount);

        Task<Transaction> DoTransactionAsync(UpdateAccountFromTransaction NewTransaction);

        Task<List<TransactionLineItem>> GetTransactionHistoryAsync(TransactionHistoryObject transactionHistoryObject, Guid id);

        Task<BankAccount> GetBankAccountAsync(Guid AcctId);

        Task<List<GetAccountModel>> GetAllBankAccountsAsync();

    }
}
    
