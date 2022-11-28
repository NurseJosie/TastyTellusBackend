namespace TastyTellusBackend.Models
{
    public class Like
    {
        public Guid Id { get; set; }
        public Recipe Recipe { get; set; }
        public User User { get; set; }
    }
}
