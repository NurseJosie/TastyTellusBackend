using Microsoft.EntityFrameworkCore;

namespace TastyTellusBackend.Data
{
    public class TastyTellusBackendContext : DbContext
    {
        public TastyTellusBackendContext(DbContextOptions<TastyTellusBackendContext> options)
            : base(options)
        {
        }

        public DbSet<TastyTellusBackend.Models.Recipe> Recipes { get; set; }
        public DbSet<TastyTellusBackend.Models.User> Users { get; set; }
        public DbSet<TastyTellusBackend.Models.Ingredient> Ingredients { get; set; }
        public DbSet<TastyTellusBackend.Models.InstructionStep> InstructionSteps { get; set; }
    }
}