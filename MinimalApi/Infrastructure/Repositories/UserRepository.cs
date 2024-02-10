using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using MinimalApi.Domain.Models;
using MinimalApi.Configurations;

namespace MinimalApi.Infrastructure.Repositories
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserRepository(IOptions<MongoDBSettings> mongoDBSettings) {
            Configuration configuration = new Configuration();
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(configuration.GetValue("MONGODB_DATABASE"));
            _userCollection = database.GetCollection<User>("users");
        }

        public async Task CreateUser(User user) {
            await _userCollection.InsertOneAsync(user);
            return;
        }

        public async Task<List<User>> GetAllUsers() {
            return await _userCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<User> GetUserById(string id) {
            return await _userCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmail(string email) {
            return await _userCollection.Find(user => user.Email == email).FirstOrDefaultAsync();
        }

        public async Task UpdateUser(User user) {
            await _userCollection.ReplaceOneAsync(x => x.Id == user.Id, user);
            return;
        }

        public async Task DeleteUser(string id) {
            await _userCollection.DeleteOneAsync(user => user.Id == id);
            return;
        }
    }
}