

using BankingApi_with_ReactFrontend.Server.Entities;
using BankingApi_with_ReactFrontend.Server.Models;

namespace BankingApi_with_ReactFrontend.Server.Services
{
    public interface ITransactionQueryService
    {
        IQueryable<Transaction>ApplyFilter(IQueryable<Transaction> filterTransactions, TransactionHistoryObject transactionQueryModel);

        IQueryable<Transaction> ApplySorting(IQueryable<Transaction> sortTransactions, TransactionHistoryObject transactionQueryModel);

        IQueryable<TransactionLineItem> PaginationControls(IQueryable<Transaction> transactionsQuery, TransactionHistoryObject transactionQueryModel);
    }
}
