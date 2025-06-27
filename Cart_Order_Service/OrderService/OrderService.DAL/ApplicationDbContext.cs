using OrderService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace OrderService.DAL
{
    public class ApplicationDbContext : DbContext
    {
        #region Tables 
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

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
            //modelBuilder.Entity<User>()
            //    .HasIndex(e => e.Email)
            //    .IsUnique();
            //modelBuilder.Entity<User>()
            //    .HasIndex(p => p.Phone)
            //    .IsUnique();
        }
    }
}
