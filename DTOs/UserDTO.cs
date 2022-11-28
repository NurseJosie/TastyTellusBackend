using System.Reflection;
using TastyTellusBackend.Models;

namespace TastyTellusBackend.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string>? LikedRecipes { get; set; }
        public bool IsAdmin { get; set; } = false; 
        
        public bool IsSignedIn { get; set; } = false;



        public UserDTO(User user)
        {
            Id = user.Id;

            Username = user.Username;

            Email = user.Email;

            LikedRecipes = user?.LikedRecipes?.Select(x => x.Recipe.Title).ToList() ?? new List<string>(); // visa endast receptnamn

            IsAdmin = user.IsAdmin;

            IsSignedIn = user.IsSignedIn;
        }
    }
}
