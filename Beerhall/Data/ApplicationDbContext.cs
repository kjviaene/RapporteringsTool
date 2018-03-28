
using Microsoft.EntityFrameworkCore;
using TrustTeamVersion4.Data.Mapping;
using TrustTeamVersion4.Models.Domain;

namespace TrustTeamVersion4.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Home> homes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new HomeConfiguration());
        }
    }
}
