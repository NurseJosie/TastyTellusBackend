using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TastyTellusBackend.Data;
using TastyTellusBackend.Models;

//https://www.youtube.com/watch?v=Fbf_ua2t6v4&t=2s

namespace TastyTellusBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //private static List<User> users = new List<User>
        //{
        //    // IF user count i DB >1, skapa dessa
        //    new User
        //    {
        //        Id = 1,
        //        Username = "Admin",
        //        Email = "tastytellus@mail.com",
        //        Password = "admin123",
        //        IsAdmin = true
        //    },
        //    new User
        //    {
        //        Id = 2,
        //        Username = "KalleNilsson",
        //        Email = "kallenilsson@mail.com",
        //        Password = "kalle123",
        //        IsAdmin = false
        //    },
        //    new User
        //    {
        //        Id = 3,
        //        Username = "SaraSvensson",
        //        Email = "sarasvensson@mail.com",
        //        Password = "sara123",
        //        IsAdmin = false
        //    },
        //};

        //private readonly TastyTellusBackendContext _context;

        //public UsersController(TastyTellusBackendContext context)
        //{
        //    _context = context;
        //}

        //// GET ALL
        //[HttpGet]
        //public async Task<ActionResult<List<User>>> GetUser()
        //{
        //    return Ok(await _context.Users.ToListAsync());
        //}

        //// GET ONE BY ID
        //[HttpGet("{id}")]
        //public async Task<ActionResult<User>> GetUser(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return BadRequest("User not found.");
        //    }
        //    return Ok(user);
        //}

        ////// POST, CREATE NEW
        ////[HttpPost]
        ////public async Task<ActionResult<List<User>>> AddUser(User user)
        ////{
        ////    _context.Users.Add(user); // funkar ej?
        ////    await _context.SaveChangesAsync();

        ////    return Ok(await _context.Users.ToListAsync());
        ////}

        //// PUT, UPDATE
        //[HttpPut]
        //public async Task<ActionResult<List<User>>> UpdateUser(User request)
        //{
        //    var dbUser = await _context.Users.FindAsync(request.Id);
        //    if (dbUser == null)
        //    {
        //        return BadRequest("User not found.");
        //    }
        //    dbUser.Username = request.Username;
        //    dbUser.Email = request.Email;
        //    dbUser.Password = request.Password; // dubbelcheck?
        //    // lägg till mer?

        //    await _context.SaveChangesAsync();

        //    return Ok(await _context.Users.ToListAsync());
        //}

        //// DELETE
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<List<User>>> DeleteUser(int id)
        //{
        //    var dbUser = await _context.Users.FindAsync(id);
        //    if (dbUser == null)
        //    {
        //        return BadRequest("User not found.");
        //    }
        //    _context.Users.Remove(dbUser);
        //    await _context.SaveChangesAsync();

        //    return Ok(await _context.Users.ToListAsync());
        //}
    }
}