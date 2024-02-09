using Microsoft.AspNetCore.Mvc;
using MinimalApi.Domain.Models;
using MinimalApi.Filters.AuthFilters;
using MinimalApi.Infrastructure.Services;

namespace MinimalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthRepository _authService;

        public AuthController(AuthRepository authService) {
            _authService = authService;
        }
        
        [HttpPost]
        [Auth_ValidateUserFilter]
        public async Task<ActionResult> Create([FromBody]User user) {
             return Ok(await _authService.Login(user));
        }

        [HttpPost]
        [Auth_ValidateTokenFilter]
        public async Task<IActionResult> Destroy(string token) {
            await _authService.Logout(token);
            return Ok();
        }
    }
}