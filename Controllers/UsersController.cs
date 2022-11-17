using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TastyTellusBackend.Data;
using TastyTellusBackend.Models;
using TastyTellusBackend.DTOs;

// uppdatera med DTO!

namespace TastyTellusBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TastyTellusBackendContext _context;

        public UsersController(TastyTellusBackendContext context)
        {
            _context = context;
        }

        // GET ALL
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users.Select(x => new UserDTO(x)).ToList());
        }

        //[HttpGet]   // gammal utan DTO
        //public async Task<ActionResult<List<User>>> GetUser()
        //{
        //    return Ok(await _context.Users.ToListAsync());
        //}

        // GET ONE BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(user);
        }

        //// POST, CREATE NEW
        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser(User user)
        {
            _context.Users.Add(user); 
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

        // PUT, UPDATE
        [HttpPut]
        public async Task<ActionResult<List<User>>> UpdateUser(User request)
        {
            var dbUser = await _context.Users.FindAsync(request.Id);
            if (dbUser == null)
            {
                return BadRequest("User not found.");
            }
            dbUser.Username = request.Username;
            dbUser.Email = request.Email;
            dbUser.Password = request.Password; // dubbelcheck?
            // lägg till mer?

            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            var dbUser = await _context.Users.FindAsync(id);
            if (dbUser == null)
            {
                return BadRequest("User not found.");
            }
            _context.Users.Remove(dbUser);
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

        ////Logga in
        //[HttpPost("Login")]
        //public async Task<IActionResult> Login([FromBody] User userLogin)
        //{
        //   await _context.Find(o => o.Username.ToLower() == userLogin.Username.ToLower() && o.Password == userLogin.Password && o.IsActive).FirstOrDefaultAsync();

        //    var user = await _context.LoginAsync(userLogin);

        //    if (user != null)
        //    {
        //        return Ok(user);
        //    }

        //    return NotFound("User not found");
        //}
    }
}

// MER ENDPOINTS
// logga in
// likea recept?
// remove like?