using Microsoft.EntityFrameworkCore;

namespace Fitness.S1.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Models.Trainer> Trainers { get; set; }
    }
}
