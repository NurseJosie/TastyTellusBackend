using System.Reflection;
using TastyTellusBackend.Models;

namespace TastyTellusBackend.DTOs
{
    public class RecipeDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string Intro { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Instructions { get; set; }
        public string SourceURL { get; set; }

        public RecipeDTO(Recipe recipe)
        {
            Id = recipe.Id;

            Title = recipe.Title;

            ImageURL = recipe.ImageURL;

            Intro = recipe.Intro;

            Ingredients = recipe.Ingredients.Select(x => x.IngredientAmount + " " + x.IngredientName).ToList(); 

            Instructions = recipe.Instructions.Select(x => x.Instruction).ToList();

            SourceURL = recipe.SourceURL;
        }
    }

    
}
