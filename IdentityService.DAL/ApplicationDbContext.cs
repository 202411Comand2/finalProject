using IdentityService.Domain;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DAL
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
        public DbSet<ShopOwner> ShopOwners { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
