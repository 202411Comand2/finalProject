using DAL.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

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

        /// <summary>
        /// Вернуть все корневые кластеры
        /// </summary>
        /// <returns>Коллецию кластеров</returns>
        public async Task<List<Cluster>> GetRootElementsClaster() 
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.Clusters.Where(c => c.ParentId == -1).ToListAsync();
            }
        }

        /// <summary>
        /// Вернуть все дочерние элементы класстера по указаному id
        /// </summary>
        /// <param name="id">Id кластера у которого нужно вернуть всех его родителей</param>
        /// <returns></returns>
        public async Task<List<Cluster>> GeElementsCluster(int id)
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.Clusters.Where(c => c.ParentId == id).ToListAsync();
            }
        }




        /// <summary>
        /// Вернуть массив кластеров
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<Cluster>> GetArrayCluster(List<int> ids) 
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.Clusters
                .Where(e => ids.Contains(e.Id)) 
                .ToListAsync();
            }
        }

    }
}
