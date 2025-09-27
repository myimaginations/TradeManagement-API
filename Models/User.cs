using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TradeManagement.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!; // MongoDB will generate

        [BsonElement("Username")]
        public required string Username { get; set; }

        [BsonElement("PasswordHash")]
        public required string PasswordHash { get; set; }

        [BsonElement("Roles")]
        public required List<string> Roles { get; set; }
    }
}
