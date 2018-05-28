
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

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Data Source = trustsq02.trustteam.local; Initial Catalog = ClienteleITSM_PROD_Application; Persist Security Info = True; User ID = servicereporting; Password = 3WvBHq3AEhIwWMAuPi; MultipleActiveResultSets = True");
		}
    }
}
