using Microsoft.AspNetCore.Mvc;
using MinimalApi.Domain.Models;

namespace MinimalApi.Infrastructure.Services
{
    public class AuthRepository
    {
        public async Task<ActionResult<User>> Login(User user) {
            return await GenerateToken(user);
        }

        public async Task<ActionResult<User>> Logout(string token) {
            return await RevokeToken(token);
        }
    }
}
