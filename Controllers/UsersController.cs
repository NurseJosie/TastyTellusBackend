using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TastyTellusBackend.Data;
using TastyTellusBackend.Models;
using TastyTellusBackend.DTOs;

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

        // GET ONE BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(Guid id)
        {
            if (id == null)
            {
                return BadRequest("Id required.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new UserDTO(user));
        }

        // POST, CREATE NEW
        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddUser(User userInput)
        {
            if (userInput == null)
            {
                return BadRequest("More information needed.");
            }

            var user = new User()
            {
                Username = userInput.Username,
                Email = userInput.Email,
                Password = userInput.Password,
                IsSignedIn = userInput.IsSignedIn,
                IsAdmin = userInput.IsAdmin,
            };

            _context.Users.Add(user); 
            await _context.SaveChangesAsync();

            return Ok(new UserDTO(user));

            // TODO: checka att username är unikt. 
        }

        // PUT, UPDATE
        [HttpPut]
        public async Task<ActionResult<UserDTO>> UpdateUser(User request) 
        {
            var dbUser = await _context.Users.FindAsync(request.Id);
            if (dbUser == null)
            {
                return BadRequest("User not found.");
            }
            dbUser.Username = request.Username;
            dbUser.Email = request.Email;
            dbUser.Password = request.Password; 

            await _context.SaveChangesAsync();

            return Ok(new UserDTO(dbUser));
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id) 
        {
            if (id == null)
            {
                return BadRequest("Id required.");
            }

            var dbUser = await _context.Users.FindAsync(id);
            if (dbUser == null)
            {
                return BadRequest("User not found.");
            }
            _context.Users.Remove(dbUser);
            await _context.SaveChangesAsync();

            return Ok();
        }

        //LOG IN
        [HttpPut("LogIn")]
        public async Task<ActionResult> LogIn(User userLogin)
        {
            var user = await _context.Users.Where(o => o.Username.ToLower() == userLogin.Username.ToLower() && o.Password == userLogin.Password).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }
    }
}

