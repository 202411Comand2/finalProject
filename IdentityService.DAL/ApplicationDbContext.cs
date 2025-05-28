using IdentityService.Domain;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DAL
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
