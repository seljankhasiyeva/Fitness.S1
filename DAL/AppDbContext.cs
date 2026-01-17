using Fitness.S1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fitness.S1.DAL
{
    public class AppDbContext: IdentityDbContext<AppUser>

    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Models.Trainer> Trainers { get; set; }
    }
}
