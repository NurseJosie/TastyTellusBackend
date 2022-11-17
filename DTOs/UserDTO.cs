using System.Reflection;
using TastyTellusBackend.Models;

namespace TastyTellusBackend.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; } //skicka ej med password till frontend?
        //public bool IsAdmin { get; set; } = false; // skicka med?

        public UserDTO(User user)
        {
            Id = user.Id;

            Username = user.Username;

            Email = user.Email;
        }
    }
}
