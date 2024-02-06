using Microsoft.AspNetCore.Mvc;
using MinimalApi.src.Database.Models;
using MinimalApi.src.Database.Services;

namespace MinimalApi.src.Application.Controllers.UsersController
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly UserService _mongoDBService;

        public UsersController(UserService mongoDBService) {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() {
            return Ok(await _mongoDBService.GetAllUsers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Show(string id) {
            return Ok(await _mongoDBService.GetUserById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]User user) {
            await _mongoDBService.CreateUser(user);
            return Ok("User created successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]User user) {
            await _mongoDBService.UpdateUser(user);
            return Ok("User information updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) {
            await _mongoDBService.DeleteUser(id);
            return Ok("User deleted successfully.");
        }
    }
}