namespace BankingApi_with_ReactFrontend.Server.Models
{
    public class TransactionHistoryObject
    {
        public int? page { get; set; }

        public int? pageSize { get; set; }

        public bool? sortDesc { get; set; }

        public bool? sortAsc { get; set; }

        public FilterBy? filterBy { get; set; }

        public SortBy? sortBy { get; set; }
    }
}
