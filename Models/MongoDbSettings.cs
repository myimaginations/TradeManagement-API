namespace TradeManagement.Models
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string UsersCollectionName { get; set; } = null!;
        public string TradesCollectionName { get; set; } = null!;
        public string OrdersCollectionName { get; set; } = null!;
        public string InstrumentsCollectionName { get; set; } = null!;
        public string PortfoliosCollectionName { get; set; } = null!;
    }
}
