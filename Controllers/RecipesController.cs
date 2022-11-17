using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TastyTellusBackend.Data;
using TastyTellusBackend.DTOs;
using TastyTellusBackend.Models;

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
        public async Task<ActionResult<List<RecipeDTO>>> GetRecipes()
        {
            var recipes = await _context.Recipes.Include(x => x.Ingredients).Include(y => y.Instructions).ToListAsync();
            return Ok(recipes.Select(x => new RecipeDTO(x)).ToList());
        }

        // GET ONE BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDTO>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return BadRequest("Recipe not found.");
            }
            return Ok(new RecipeDTO(recipe));
            // behövs Recipes.Include(x => x.Ingredients).Include(y => y.Instructions).ToListAsync(); för att visa ing och inst?
        }

        // POST, CREATE NEW
        [HttpPost]
        public async Task<ActionResult<List<Recipe>>> AddRecipe(Recipe recipeInput)
        {
            var recipe = new Recipe()
            {
                Title = recipeInput.Title,
                ImageURL = recipeInput.ImageURL,
                Intro = recipeInput.Intro,
                Ingredients = new List<Ingredient>(),
                Instructions = new List<InstructionStep>(),
                SourceURL = recipeInput.SourceURL,
            };
            recipe.Ingredients = recipeInput.Ingredients.Select(x => new Ingredient() { IngredientAmount = x.IngredientAmount, IngredientName = x.IngredientName }).ToList();
            recipe.Instructions = recipeInput.Instructions.Select(x => new InstructionStep() { Instruction = x.Instruction }).ToList();
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // PUT, UPDATE
        [HttpPut]
        public async Task<ActionResult<List<Recipe>>> UpdateRecipe(Recipe request)
        {
            var dbRecipe = await _context.Recipes.FindAsync(request.Id);
            //var dbRecipe = await _context.Recipes.Where(x => x.Id == request.Id).Include(x => x.Ingredients).Include(y => y.Instructions).FirstOrDefaultAsync();
            if (dbRecipe == null)
            {
                return BadRequest("Recipe not found.");
            }

            //_context.InstructionSteps.RemoveRange(dbRecipe.Instructions);
            //_context.Ingredients.RemoveRange(dbRecipe.Ingredients);

            dbRecipe.Title = request.Title;
            dbRecipe.ImageURL = request.ImageURL;
            dbRecipe.Intro = request.Intro;
            dbRecipe.SourceURL = request.SourceURL;
            dbRecipe.Ingredients = request.Ingredients.Select(x => new Ingredient() { IngredientAmount = x.IngredientAmount, IngredientName = x.IngredientName }).ToList();
            dbRecipe.Instructions = request.Instructions.Select(x => new InstructionStep() { Instruction = x.Instruction }).ToList();

            await _context.SaveChangesAsync();

            return Ok();
        }

        // fungerar ej!!!!
        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Recipe>>> DeleteRecipe(Guid id)
        {
            var dbRecipe = await _context.Recipes.FindAsync(id);
            //var dbRecipe = await _context.Recipes.Where(x => x.Id == request.Id).Include(x => x.Ingredients).Include(y => y.Instructions).FirstOrDefaultAsync();
            if (dbRecipe == null)
            {
                return BadRequest("Recipe not found.");
            }
            //_context.InstructionSteps.RemoveRange(dbRecipe.Instructions);
            //_context.Ingredients.RemoveRange(dbRecipe.Ingredients);
            _context.Recipes.Remove(dbRecipe);
            await _context.SaveChangesAsync();

            return Ok(await _context.Recipes.ToListAsync());
        }
    }
}

// MER ENDPOINTS
// likea recept
// söka på recept efter ord i titel eller ingrediens
// sortera recept på mest likes