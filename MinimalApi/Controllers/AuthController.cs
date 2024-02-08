using Microsoft.AspNetCore.Mvc;
using MinimalApi.Domain.Models;
using MinimalApi.Infrastructure.Services;

namespace MinimalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService) {
            _authService = authService;
        }
        
        [HttpPost]
        public async Task<ActionResult> Login([FromBody]User user) {
             return Ok(await _authService.Login(user));
        }

    }
}