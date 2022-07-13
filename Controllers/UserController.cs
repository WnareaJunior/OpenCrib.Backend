using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OpenCrib.api.Models;
using OpenCrib.api.Services;
//using System.Web.Http;

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
        [Route("GetAllUsers")]
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

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }

    }

    [ApiController]
    [Route("api/[controller]")]
    public class PartyController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;

        public PartyController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        [Route("GetNearbyParties/{postalCode}")]
        public async Task<List<Party>> GetNearbyParties(string postalCode)
        {


            return await _mongoDBService.PartiesInsideZip(postalCode);


        }

    }

}
