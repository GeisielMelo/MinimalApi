using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Domain.Models;
using MinimalApi.Filters.UserFilters;
using MinimalApi.Infrastructure.Repositories;

namespace MinimalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly UserRepository _mongoDBService;

        public UsersController(UserRepository mongoDBService) {
            _mongoDBService = mongoDBService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index() {
            return Ok(await _mongoDBService.GetAllUsers());
        }

        [Authorize]
        [HttpGet("{id}")]
        [User_ValidateUserIdFilter]
        public async Task<IActionResult> Show(string id) {
            return Ok(await _mongoDBService.GetUserById(id));
        }

        [HttpPost]
        [User_ValidateRegisteredEmailFilter]
        public async Task<IActionResult> Create([FromBody]User user) {
            await _mongoDBService.CreateUser(user);
            return Ok("User created successfully.");
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]User user) {
            await _mongoDBService.UpdateUser(user);
            return Ok("User information updated successfully.");
        }

        [Authorize]
        [HttpDelete("{id}")]
        [User_ValidateUserIdFilter]
        public async Task<IActionResult> Delete(string id) {
            await _mongoDBService.DeleteUser(id);
            return Ok("User deleted successfully.");
        }
    }
}