using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TradeManagement.Models
{
    public class Instrument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("symbol")]
        public string Symbol { get; set; } = string.Empty;

        [BsonElement("price")]
        public decimal Price { get; set; }
    }
}
