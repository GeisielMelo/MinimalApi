using Microsoft.AspNetCore.Mvc;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController: ControllerBase
    {
        [HttpGet]
        public string Index()
        {
            return "Index: Indexed all the shirts.";
        }

        [HttpGet("{id}")]
        public string Show(int id)
        {
            return $"Show: {id}";
        }

        [HttpPost]
        public string Create()
        {
            return "Create: Created.";
        }

        [HttpPut("{id}")]
        public string Update(int id)
        {
            return $"Update: {id}.";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"Delete: {id}.";
        }
    }
}
