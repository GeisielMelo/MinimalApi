using MinimalApi.Application.Services;
using MinimalApi.Domain.Models;

namespace MinimalApi.Infrastructure.Repositories
{
    public class AuthRepository
    {
        private readonly TokenService _tokenService;

        public AuthRepository(TokenService tokenService){
            _tokenService = tokenService;
        }

        public async Task<string> Login(User user) {
            return await _tokenService.GenerateToken(user);
        }
    }
}
