using Microsoft.EntityFrameworkCore;

namespace TastyTellusBackend.Data
{
    public class TastyTellusBackendContext : DbContext
    {
        public TastyTellusBackendContext(DbContextOptions<TastyTellusBackendContext> options)
            : base(options)
        {
        }

        public DbSet<TastyTellusBackend.Model.Recipe> Recipes { get; set; }
        //public DbSet<TastyTellusBackend.Model.User> Users { get; set; }
    }
}