using Cogtive.App.Models;
using Microsoft.EntityFrameworkCore;

namespace Cogtive.App.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Machine> Machines { get; set; }
        public DbSet<ProductionData> ProductionData { get; set; }
        public DbSet<PendingOperation> PendingOperations { get; set; }

    }
}
