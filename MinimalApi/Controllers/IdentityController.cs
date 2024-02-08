using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace MinimalApi.Controllers
{
    public class IdentityController : ControllerBase
    {
        private const string TokenSecret = "my-secret-token-to-change-in-production";
        private static readonly TimeSpan TokenExpiration = TimeSpan.FromMinutes(5);

        [HttpPost("token")]
        public IActionResult GenerateToken([FromBody] TokenGeneration request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(TokenSecret);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Sub, request.Email)
                }),
                Expires = DateTime.UtcNow.Add(TokenExpiration),
                Issuer = "admin",
                Audience = "admin",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);
            return Ok(jwt);
        }
    }

    public class TokenGeneration
    {
        public string? Email { get; set; }
    }
}
