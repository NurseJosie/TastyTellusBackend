using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TastyTellusBackend.Data;
using TastyTellusBackend.Model;

namespace TastyTellusBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly TastyTellusBackendContext _context;

        public RecipesController(TastyTellusBackendContext context)
        {
            _context = context;
        }

        // GET ALL
        [HttpGet]
        public async Task<ActionResult<List<Recipe>>> GetRecipe()
        {
            return Ok(await _context.Recipes.ToListAsync());
        }

        // GET ONE BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return BadRequest("Recipe not found.");
            }
            return Ok(recipe);
        }

        // POST, CREATE NEW
        [HttpPost]
        public async Task<ActionResult<List<Recipe>>> AddRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return Ok(await _context.Recipes.ToListAsync());
        }

        // PUT, UPDATE
        [HttpPut]
        public async Task<ActionResult<List<Recipe>>> UpdateRecipe(Recipe request)
        {
            var dbRecipe = await _context.Recipes.FindAsync(request.Id);
            if (dbRecipe == null)
            {
                return BadRequest("Recipe not found.");
            }
            dbRecipe.Title = request.Title;
            dbRecipe.ImageURL = request.ImageURL;
            dbRecipe.Intro = request.Intro;
            dbRecipe.Ingredients = request.Ingredients;
            dbRecipe.Instructions = request.Instructions;
            dbRecipe.SourceURL = request.SourceURL;
            // lägg till mer?

            await _context.SaveChangesAsync();

            return Ok(await _context.Recipes.ToListAsync());
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Recipe>>> DeleteRecipe(int id)
        {
            var dbRecipe = await _context.Recipes.FindAsync(id);
            if (dbRecipe == null)
            {
                return BadRequest("Recipe not found.");
            }
            _context.Recipes.Remove(dbRecipe);
            await _context.SaveChangesAsync();

            return Ok(await _context.Recipes.ToListAsync());
        }
    }
}