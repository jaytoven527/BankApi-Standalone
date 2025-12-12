namespace BankingApi_with_ReactFrontend.Server.Models
{
    
    public class SortOrientation
    {
        public bool sortDes { get; set; } = false;

        public bool sortAsc { get; set; } = false;
    }

    public enum TransactionType
    {
        Withdraw,
        Deposit,
    }
    
    public enum SortBy
    {
        Date,
        Amount,
    }

    public enum FilterBy
    {
        Withdraw,
        Deposit,
    }
}
