using TastyTellusBackend.Model;

namespace TastyTellusBackend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; } = false;
        public List<Recipe> LikedRecipes { get; set; } // LIST
                                                       // public List<Recipe> Comments { get; set; } // lagra själva kommentaren (och vilket recept) LIST ????
    }
}