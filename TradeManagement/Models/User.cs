using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TradeManagement.Models
{
    public class User
    {
        [BsonId] // MongoDB primary key
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("username")]
        public string Username { get; set; } = string.Empty;

        [BsonElement("balance")]
        public decimal Balance { get; set; }
    }
}
