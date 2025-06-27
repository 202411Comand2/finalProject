using ClusterService.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClusterService.DAL
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
                Cluster? cluster = await context.Cluster
                    .FirstOrDefaultAsync(p => p.Name == name);
                return cluster;
            }
        }

        /// <summary>
        /// Получить всё дочерние кластеры
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<List<Cluster>> GetChildClusterIdsOptimized(int parentId)
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.Cluster
                .Where(c => c.ParentId == parentId)
                .ToListAsync();
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
                return await context.Cluster.Where(c => c.ParentId == -1).ToListAsync();
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
                return await context.Cluster.Where(c => c.ParentId == id).ToListAsync();
            }
        }


        /// <summary>
        /// Вернуть список кластеров
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public async Task<List<Cluster>> GetSearchCluster(string keyWord) 
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.SearchClusters
                .Where(sc => sc.KeyWord.Contains(keyWord))
                .SelectMany(sc => sc.Links)
                .Select(l => l.Cluster)
                .Distinct()
                .ToListAsync();
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
                return await context.Cluster
                .Where(e => ids.Contains(e.Id)) 
                .ToListAsync();
            }
        }

    }
}
