namespace BankingApi_with_ReactFrontend.Server.Models
{
    public class TransactionHistoryObject
    {
        public int page { get; set; }

        public int pageSize { get; set; }

        public int totalCount { get; set; }

        public Guid bankAcctId { get; set; }

        public FilterBy? filterBy { get; set; }

        public SortBy? sortBy { get; set; }
    }
}
