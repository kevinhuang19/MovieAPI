using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace MovieAPI.Models
{
    [BsonIgnoreExtraElements]
    public class Movies
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("imdbId")]
        public string? ImdbId { get; set; }

        [BsonElement("title")]
        public string? Title { get; set; }

        [BsonElement("releaseDate")]
        public string? ReleaseDate { get; set; }

        [BsonElement("trailerLink")]
        public string? TrailerLink { get; set; }

        [BsonElement("genres")]
        public List<string>? Genres { get; set; }

        [BsonElement("poster")]
        public string? Poster { get; set; }

        [BsonElement("backdrops")]
        public List<string>? Backdrops { get; set; }

        [BsonElement("reviewIds")]
        public List<Review>? ReviewIds { get; set; }

    }
}
