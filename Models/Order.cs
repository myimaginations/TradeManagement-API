using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TradeManagement.Models
{
    public class Order
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

        [BsonElement("OrderType")]
        public string OrderType { get; set; } = "Buy"; // Buy or Sell

        [BsonElement("Status")]
        public string Status { get; set; } = "Pending";

        [BsonElement("TraderId")]
        public string TraderId { get; set; } = string.Empty;

        [BsonElement("OrderDate")]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    }
}
