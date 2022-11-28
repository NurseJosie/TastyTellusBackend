using System.ComponentModel.DataAnnotations;
using TastyTellusBackend.Models;

namespace TastyTellusBackend.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required.")]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public List<Like>? LikedRecipes { get; set; }
        public bool IsSignedIn { get; set; }
    }
}