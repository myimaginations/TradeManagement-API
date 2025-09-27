using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TradeManagement.Models
{
    public class Portfolio
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("TraderId")]
        public string TraderId { get; set; } = string.Empty;

        [BsonElement("Holdings")]
        public List<PortfolioHolding> Holdings { get; set; } = new List<PortfolioHolding>();
    }

    public class PortfolioHolding
    {
        public string InstrumentId { get; set; } = string.Empty;
        public double Quantity { get; set; } = 0;
        public double AveragePrice { get; set; } = 0;
    }
}
