using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TradeManagement.Models
{
    public class Instrument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("Name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("Symbol")]
        public string Symbol { get; set; } = string.Empty;

        [BsonElement("Market")]
        public string Market { get; set; } = string.Empty;

        [BsonElement("Price")]
        public double Price { get; set; } = 0;
    }
}
