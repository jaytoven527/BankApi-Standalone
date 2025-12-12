namespace BankingApi_with_ReactFrontend.Server.Models
{

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
