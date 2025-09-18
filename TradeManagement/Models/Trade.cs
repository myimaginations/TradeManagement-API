using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TradeManagement.Models
{
    public class Trade
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("symbol")]
        public string Symbol { get; set; }  // e.g. "AAPL", "TSLA"

        [BsonElement("type")]
        public string Type { get; set; }    // "BUY" or "SELL"

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
