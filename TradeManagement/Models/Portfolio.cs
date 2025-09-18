using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TradeManagement.Models
{
    public class Portfolio
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = string.Empty;

        [BsonElement("holdings")]
        public List<Holding> Holdings { get; set; } = new List<Holding>();
    }

    public class Holding
    {
        [BsonElement("symbol")]
        public string Symbol { get; set; } = string.Empty;

        [BsonElement("quantity")]
        public int Quantity { get; set; }
    }
}
