using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MinimalApi.Domain.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace MinimalApi.Infrastructure.Services
{
    public class AuthService
    {
        private readonly IMongoCollection<User> _userCollection;

        public AuthService(IOptions<MongoDBSettings> mongoDBSettings) {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _userCollection = database.GetCollection<User>("users");
        }
        public async Task<ActionResult<User>> Login(User user) {
            var filter = Builders<User>.Filter.Eq(u => u.Email, user.Email);
            var data = await _userCollection.Find(filter).FirstOrDefaultAsync();
 
            if (data != null && data.Password == user.Password) {
                return data;
            } else {
                return new NotFoundResult();
            }
        }
    }
}
