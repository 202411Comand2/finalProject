using DAL.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ClusterRepository : BaseRepository<Cluster>
    {
        public ClusterRepository(IContextManager manager) : base(manager)
        {

        }
        /// <summary>
        /// Получить класстер(классификатор) по наименованию
        /// </summary>
        /// <param name="name">Название кластера</param>
        /// <returns>Task<Cluster></returns>
        public async Task<Cluster> GetNameCluster(string name) 
        {
            using (var context = CreateDatabaseContext())
            {
                Cluster? cluster = await context.Clusters
                    .FirstOrDefaultAsync(p => p.Name == name);
                return cluster;
            }
            
        }
    }
}
