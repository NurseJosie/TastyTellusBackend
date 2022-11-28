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
        public async Task<ActionResult<RecipeDTO>> GetRecipe(Guid id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return BadRequest("Recipe not found.");
            }
            return Ok(new RecipeDTO(recipe));
        }

        // POST, CREATE NEW
        [HttpPost]
        public async Task<ActionResult<RecipeDTO>> AddRecipe(Recipe recipeInput)
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

            return Ok(new RecipeDTO(recipe));
        }

        // PUT, UPDATE
        [HttpPut]
        public async Task<ActionResult<RecipeDTO>> UpdateRecipe(Recipe request)
        {
            var dbRecipe = await _context.Recipes.Where(x => x.Id == request.Id).Include(x => x.Ingredients).Include(y => y.Instructions).FirstOrDefaultAsync();
            if (dbRecipe == null)
            {
                return BadRequest("Recipe not found.");
            }

            dbRecipe.Title = request.Title;
            dbRecipe.ImageURL = request.ImageURL;
            dbRecipe.Intro = request.Intro;
            dbRecipe.SourceURL = request.SourceURL;

            //https://stackoverflow.com/questions/21592596/update-multiple-rows-in-entity-framework-from-a-list-of-ids

            dbRecipe.Ingredients.Where(x => request.Ingredients.Contains(x)).ToList().ForEach(y => {
                y.IngredientName = y.IngredientName = request.Ingredients.Find(z => z.Id == y.Id)?.IngredientName ?? y.IngredientName;
                y.IngredientAmount = y.IngredientAmount = request.Ingredients.Find(z => z.Id == y.Id)?.IngredientAmount ?? y.IngredientAmount;
            });

            dbRecipe.Instructions.Where(x => request.Instructions.Contains(x)).ToList().ForEach(y => {
                y.Instruction = y.Instruction = request.Instructions.Find(z => z.Id == y.Id)?.Instruction ?? y.Instruction;
            });

            await _context.SaveChangesAsync();

            return Ok(new RecipeDTO(dbRecipe));
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecipe(Guid id)
        {
            if (id == null)
            {
                return BadRequest("Id required.");
            }

            var dbRecipe = await _context.Recipes.Where(x => x.Id == id).Include(x => x.Ingredients).Include(y => y.Instructions).FirstOrDefaultAsync();
            if (dbRecipe == null)
            {
                return BadRequest("Recipe not found.");
            }
            _context.InstructionSteps.RemoveRange(dbRecipe.Instructions);
            _context.Ingredients.RemoveRange(dbRecipe.Ingredients);
            _context.Recipes.Remove(dbRecipe);
            await _context.SaveChangesAsync();

            return Ok();
        }

        //SEARCH BY TITLE OR INGREDIENT
        [HttpGet("GetRecipesSearch")]
        public async Task<ActionResult<List<RecipeDTO>>> GetRecipesSearch(string searchInput)
        {
            if (searchInput == null)
            {
                return BadRequest("Input required.");
            }

            var result = await _context.Recipes
                .Include(x => x.Ingredients)
                .Include(y => y.Instructions)
                .Where(x => x.Title.Contains(searchInput) || x.Ingredients.Any(y => y.IngredientName.Contains(searchInput)))
                .ToListAsync();

            return Ok(result);
        }

        // BEHÖVS INTE? ta bort ---------------------------------------------------------------------------------------------------------------------------------------------------------

        //// GET ALL RECIPES LIKED BY USER
        //[HttpGet("LikedRecipes")]
        //public async Task<ActionResult<List<string>>> GetLikedRecipes(Guid userId)
        //{
        //    if (userId == null)
        //    {
        //        return BadRequest("User id required.");
        //    }

        //    var dbUser = await _context.Users.FindAsync(userId);
        //    if (dbUser == null)
        //    {
        //        return BadRequest("User not found.");
        //    }

        //    return Ok(dbUser?.LikedRecipes?.Select(x => x.Recipe.Title).ToList() ?? new List<string>());

        //    // lista alla receptnamn som en spec. user har likat (redan klart?)
        //    // lista specifik användares likade recept-titlar, likedRecipes
        //}

        //// GET NUMBER OF LIKES
        //[HttpGet]
        //public async Task<ActionResult<List<Like>>> GetLikes()
        //{
        //    return Ok(await _context.Likes.ToListAsync());

        //    //visa antal likes på ett recept (redan klart?)
        //}



    }
}
