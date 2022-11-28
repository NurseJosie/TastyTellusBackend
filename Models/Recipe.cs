using TastyTellusBackend.Models;

namespace TastyTellusBackend.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string Intro { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<InstructionStep> Instructions { get; set; }
        public string SourceURL { get; set; }
        public List<Like> Likes { get; set; } // ANTAL likes
    }
}