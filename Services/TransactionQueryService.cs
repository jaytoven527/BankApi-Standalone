using BankingApi_with_ReactFrontend.Server.Entities;
using BankingApi_with_ReactFrontend.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace BankingApi_with_ReactFrontend.Server.Services
{
    public class TransactionQueryService : ITransactionQueryService
    {
        public IQueryable<Transaction> ApplyFilter(IQueryable<Transaction> filterTransactions, TransactionHistoryObject transactionQueryModel)
        {
            return transactionQueryModel.filterBy switch
            {
                FilterBy.Deposit => filterTransactions.Where(t => t.Type == TransactionType.Deposit),

                FilterBy.Withdraw => filterTransactions.Where(t => t.Type == TransactionType.Withdraw),
                _  => filterTransactions,
            };

        }

        public IQueryable<Transaction> ApplySorting(IQueryable<Transaction> sortTransactions, TransactionHistoryObject transactionQueryModel)
        {
            var sortAsc  = transactionQueryModel.sortAsc ?? false;
            var sortDesc = transactionQueryModel.sortDesc ?? false;



            switch (transactionQueryModel.sortBy)
            {
                case SortBy.Date:
                    return (sortAsc, sortDesc) switch
                    {
                        (true, false) => sortTransactions.OrderBy(t => t.CreatedOn),
                        (false, true) => sortTransactions.OrderByDescending(t => t.CreatedOn),
                        _ => sortTransactions.OrderByDescending(t => t.CreatedOn),
                    };
                case SortBy.Amount:
                    return (sortAsc, sortDesc) switch
                    {
                        (true, false) => sortTransactions.OrderBy(t => t.Amount),
                        (false, true) => sortTransactions.OrderByDescending(t => t.Amount),
                        _ => sortTransactions.OrderByDescending(t => t.Amount),
                    };
                default:
                    return sortTransactions.OrderByDescending(t => t.CreatedOn);


            }
            
        }

        public IQueryable<TransactionLineItem> PaginationControls(IQueryable<Transaction> transactionsQuery, TransactionHistoryObject transactionQueryModel)
        {
            var pageSize = transactionQueryModel.pageSize ?? 10;
            var page     = transactionQueryModel.page ?? 1;

            var jumpCount = (page - 1) * pageSize;

            transactionsQuery = transactionsQuery.Skip(jumpCount).Take(pageSize);

            var lineItems = transactionsQuery.Select(t => new TransactionLineItem
            {
                TransactionType = t.Type,

                Amount = t.Amount,

                TransactionDate = t.CreatedOn,

                BankAccountType = t.BankAccount.Type,

                BankAccountId = t.BankAccountId,

                TransactionId = t.Id,
            });

            return lineItems;
        }
    }
}
