using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using MinimalApi.src.Database.Models;

namespace MinimalApi.src.Database.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserService(IOptions<MongoDBSettings> mongoDBSettings) {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
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