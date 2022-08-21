using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OpenCrib.api.Models;
using OpenCrib.api.Services;

//using System.Web.Http;

namespace TestAPI.Controllers
{
    //lol
    [ApiController]
    [Route("api/[controller]")]


    public class UserController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;

        public UserController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }


        //
        // Returns all Users in the collection
        //
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<List<User>> GetAll()
        {
            return await _mongoDBService.GetAllAsync();
        }

        //
        // Makes your account follow a user 
        //
        [HttpGet]
        [Route("FollowUser/{myUserId}/{userId}")]
        public async Task FollowUser(string myUserId,string userId)
        {
            await FollowUser(myUserId,userId);
        }

        //
        // blocks a user
        //
        [HttpGet]
        [Route("BlockUser")]

        [HttpPost]
        [Route("BlockUser")]

        //
        // Returns the info of a user based on if it is available
        //
        [HttpGet]
        [Route("GetUser/{username}")]
        public async Task<User> GetUser(string username) 
        { 
        
            return await _mongoDBService.GetUserAsync(username);  
        
        }

        //
        // Adds a new user
        //need to add validations for new users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            await _mongoDBService.CreateAsync(user);
            return CreatedAtAction(nameof(GetAll), new {id = user.Id}, user);
        }

        //
        // Deletes a user
        //
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

        //
        // Creates a new party
        //
        [HttpPost]
        [Route("NewParty")]

        //
        // rsvps a party
        //
        [HttpGet]
        [Route("Rsvp/{myUserId}/{partyId}")]

        public void Rsvp(string myUserId, string partyId)
        {
            _mongoDBService.CreateRsvp(myUserId, partyId);
        }

        //
        // Whitelists people for your party
        //
        [HttpGet]
        [Route("AllowIntoParty")]

        //
        // Posts comment to a posted party
        //
        [HttpPost]
        [Route("PostComment")]

        //
        // Get Parties Nearby using your zipcode
        //
        [HttpGet]
        [Route("GetPartiesNearby/{zipCode}/{range}")]
        public async Task<List<Party>> GetPartiesNearby(string zipcode, int range)
        {
            

            

            return await _mongoDBService.PartiesNearZip(zipcode, range);


        }

    }

}
