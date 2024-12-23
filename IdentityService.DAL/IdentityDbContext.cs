using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DAL
{
	public class IdentityDbContext : DbContext, IIdentityDbContext
	{
		public DbSet<User> Users { get; set; }

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
