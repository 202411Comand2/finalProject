using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
	public class ApplicationDbContext : DbContext
	{
        #region Tables 
        public DbSet<User> Users { get; set; }
        public DbSet<ShopOwner> ShopOwners { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Cluster> Clusters { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentReply> CommentReply { get; set; }
        public DbSet<Favorite> Favorite { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<SearchCluster> SearchCluster { get; set; }

        #endregion

        public ApplicationDbContext(DbContextOptions options) : base(options) => Database.EnsureCreated();
        /// <summary>
        /// Создание контекста
        /// </summary>
        /// <param name="options">Св</param>
        /// <param name="flag"></param>
        public ApplicationDbContext(DbContextOptions options, bool flag) : base(options) 
        {
            if (flag)
            {
                Database.EnsureDeleted(); // удаление бд
                Database.EnsureCreated(); // создание бд
            }
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<User>()
                .HasIndex(e => e.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(p => p.Phone)
                .IsUnique();
		}
	}
}
