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

        [HttpGet]
        [Route("FollowUser/{myUserId}/{userId}")]
        public async Task FollowUser(string myUserId,string userId)
        {
            await FollowUser(myUserId,userId);
        }

        [HttpGet]
        [Route("BlockUser")]

        [HttpPost]
        [Route("BlockUser")]

        [HttpGet]
        [Route("GetUser/{username}")]
        public async Task<User> GetUser(string username) 
        { 
        
            return await _mongoDBService.GetUserAsync(username);  
        
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

        [HttpPost]
        [Route("NewParty")]

        [HttpGet]
        [Route("Rsvp/{myUserId}/{partyId}")]

        public void Rsvp(string myUserId, string partyId)
        {
            _mongoDBService.CreateRsvp(myUserId, partyId);
        }

        [HttpGet]
        [Route("AllowIntoParty")]

        [HttpPost]
        [Route("PostComment")]

        [HttpGet]
        [Route("GetPartiesNearby/{zipCode}/{range}")]
        public async Task<List<Party>> GetPartiesNearby(string zipcode, int range)
        {
            

            

            return await _mongoDBService.PartiesNearZip(zipcode, range);


        }

    }

}
