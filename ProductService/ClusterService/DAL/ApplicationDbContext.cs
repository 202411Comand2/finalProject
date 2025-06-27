using ClusterService.Domain;
using Microsoft.EntityFrameworkCore;

namespace ClusterService.DAL
{
	public class ApplicationDbContext : DbContext
	{
        #region Tables 
        public DbSet<Cluster> Cluster { get; set; }
        public DbSet<SearchCluster> SearchClusters { get; set; }
        public DbSet<Link> Links { get; set; }

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
            // Настройка связи многие-ко-многим через Link
            modelBuilder.Entity<Link>()
                .HasOne(l => l.Cluster)
                .WithMany(c => c.Links)
                .HasForeignKey(l => l.ClusterId);

            modelBuilder.Entity<Link>()
                .HasOne(l => l.SearchCluster)
                .WithMany(sc => sc.Links)
                .HasForeignKey(l => l.SearchClusterId);

            // Опционально: индекс для ускорения поиска по ключевым словам
            modelBuilder.Entity<SearchCluster>()
                .HasIndex(sc => sc.KeyWord);
        }
	}
}
