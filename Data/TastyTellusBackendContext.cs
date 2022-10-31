using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TastyTellusBackend.Model;

namespace TastyTellusBackend.Data
{
    public class TastyTellusBackendContext : DbContext
    {
        public TastyTellusBackendContext (DbContextOptions<TastyTellusBackendContext> options)
            : base(options)
        {
        }

        public DbSet<TastyTellusBackend.Model.Recipe> Recipe { get; set; } = default!;
    }
}
