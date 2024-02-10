using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MinimalApi.Domain.Models;

namespace MinimalApi.Application.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public TokenService(IConfiguration configuration, JwtSecurityTokenHandler tokenHandler)
        {
            _configuration = configuration;
            _tokenHandler = tokenHandler;
        }

        public async Task<string> GenerateToken(User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = "https://github.com/GeisielMelo",
                Audience = "https://github.com/GeisielMelo",
                SigningCredentials = GetSigningCredentials()
            };

            var token = _tokenHandler.CreateToken(tokenDescriptor);
            return await Task.FromResult(_tokenHandler.WriteToken(token));
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = _configuration["Jwt:Key"];

            if (secret == null)
            {
                throw new ArgumentNullException(nameof(secret));
            }
            var key = Encoding.UTF8.GetBytes(secret);
            return new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
