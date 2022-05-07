using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenCrib.api.Models;
using OpenCrib.api.Services;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   

    public class UserController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;

        public UserController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<User>> GetAll()
        {
            return await _mongoDBService.GetAllAsync();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            await _mongoDBService.CreateAsync(user);
            return CreatedAtAction(nameof(GetAll), new {id = user.Id}, user);
        }
        
        
    }
}
