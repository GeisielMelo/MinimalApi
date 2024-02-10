using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MinimalApi.Configurations;
using MinimalApi.Domain.Models;

namespace MinimalApi.Application.Services
{
    public class TokenService
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public TokenService(JwtSecurityTokenHandler tokenHandler)
        {
            _tokenHandler = tokenHandler;
        }

        public async Task<string> GenerateToken(User user)
        {
            var configuration = new Configuration();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = configuration.GetValue("JWT_ISSUER"),
                Audience = configuration.GetValue("JWT_AUDIENCE"),
                SigningCredentials = GetSigningCredentials()
            };

            var token = _tokenHandler.CreateToken(tokenDescriptor);
            return await Task.FromResult(_tokenHandler.WriteToken(token));
        }

        private SigningCredentials GetSigningCredentials()
        {        
            var configuration = new Configuration();
            var key = Encoding.UTF8.GetBytes(configuration.GetValue("JWT_SECRET"));
            return new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
