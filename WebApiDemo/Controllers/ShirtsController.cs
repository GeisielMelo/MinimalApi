using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Filters;
using WebApiDemo.Models;
using WebApiDemo.Models.Repositories;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase {
        [HttpGet]
        public IActionResult Index() {
            return Ok(ShirtRepository.GetShirts());
        }

        [HttpGet("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult Show(int id) {
            return Ok(ShirtRepository.GetShirtById(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody]Shirt shirt) {
            return Ok("Create: Created.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id) {
            return Ok($"Update: {id}.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            return Ok($"Delete: {id}.");
        }
    }
}
