using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TradeManagement.Models
{
    public class Trade
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("InstrumentId")]
        public string InstrumentId { get; set; } = string.Empty;

        [BsonElement("Quantity")]
        public double Quantity { get; set; } = 0;

        [BsonElement("Price")]
        public double Price { get; set; } = 0;

        [BsonElement("TradeDate")]
        public DateTime TradeDate { get; set; } = DateTime.UtcNow;

        [BsonElement("TraderId")]
        public string TraderId { get; set; } = string.Empty;
    }
}
