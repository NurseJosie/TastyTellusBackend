using Microsoft.AspNetCore.Mvc;
using TastyTellusBackend.Data;
using TastyTellusBackend.Models;

namespace TastyTellusBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : Controller
    {
        private readonly TastyTellusBackendContext _context;

        public LikesController(TastyTellusBackendContext context)
        {
            _context = context;
        }

        // POST, CREATE NEW
        [HttpPost]
        public async Task<ActionResult> AddLike(Like likeInput)
        {
            var like = new Like()
            {
                Recipe = likeInput.Recipe,
                User = likeInput.User
            };
            _context.Likes.Add(like);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLike(Guid id)
        {
            var dbLike = await _context.Likes.FindAsync(id);
            if (dbLike == null)
            {
                return BadRequest("Like not found.");
            }
            _context.Likes.Remove(dbLike);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}