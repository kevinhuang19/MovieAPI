using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieAPI.Models
{
    public class Review
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("body")]
        public string Body { get; set; }

        public Review(string body)
        {
            Body = body;
        }
    }
}
