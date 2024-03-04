using MovieAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MovieAPI.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Movies> _movieCollection;
        private readonly IMongoCollection<Review> _reviewCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDbSettings)
        {
            MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _movieCollection = database.GetCollection<Movies>(mongoDbSettings.Value.CollectionName);
            _reviewCollection = database.GetCollection<Review>("reviews");
        }

        public async Task<List<Movies>> getAllMovies()
        {
            return await _movieCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Movies> getSingleMovie(string imdbId)
        {
            var filter = Builders<Movies>.Filter.Eq(m => m.ImdbId, imdbId);
            return await _movieCollection.Find(filter).FirstOrDefaultAsync();
        }

        public Review createReview(string reviewBody, string imdbId)
        {
            var review = new Review(reviewBody);
            _reviewCollection.InsertOne(review);

            var movieFilter = Builders<Movies>.Filter.Eq(m => m.ImdbId, imdbId);
            var movieUpdate = Builders<Movies>.Update.Push(m => m.ReviewIds, review);
            _movieCollection.UpdateOne(movieFilter, movieUpdate);

            return review;
        }
    }
}
