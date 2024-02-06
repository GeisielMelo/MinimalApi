using System.ComponentModel.DataAnnotations;
using MinimalApi.Domain.Validations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MinimalApi.Domain.Models
{
    public class User
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        [User_EnsureCorrectEmail]
        public string Email { get; set; } = null!;

        [Required]
        [User_EnsureCorrectPassword]
        public string Password { get; set; } = null!;
    }
}