using ClusterService.Domain;
using Microsoft.EntityFrameworkCore;

namespace ClusterService.DAL
{
    public class SearchClusterRepository : BaseRepository<SearchCluster>
    {
        public SearchClusterRepository(IContextManager manager) : base(manager)
        {

        }

        /// <summary>
        /// Создать кластер поиска
        /// </summary>
        /// <param name="idCluster"></param>
        /// <param name="keyWords"></param>
        /// <returns>Список созданных объектов для прикрепления их к кластеру</returns>
        public async Task<bool> AddSearchElements(int idCluster, List<string> keyWords) 
        {
            using (var context = CreateDatabaseContext())
            {
                //пакетное создание объектов для увеличение скорости и уменьшения задержки
                var tagEntities = keyWords.Select(t => new SearchCluster { KeyWord = t, ClusterId = idCluster }).ToList();
                await context.searchClusters.AddRangeAsync(tagEntities); // Пакетное добавление
                var sds = await context.SaveChangesAsync(); // Сохраняем изменения
                return true;
            }
            return false;   
        }

        /// <summary>
        /// Пакетное удаление данных
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<bool> AddSearchElements(List<int> ids)
        {
            using (var context = CreateDatabaseContext())
            {
                await context.searchClusters
                   .Where(e => ids.Contains(e.Id))
                   .ExecuteDeleteAsync();
                return true;
            }
        }

        /// <summary>
        /// Получить все ключевые кластера по которым идёт поиск
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<SearchCluster>> GetSearchClusters(int id) 
        {
            using (var context = CreateDatabaseContext())
            {
               return await context.searchClusters
                .Where(e => e.ClusterId == id).ToListAsync();
            }
        }
        /// <summary>
        /// Проверить, что хотя бы у этого кластера есть хоть одно слова для поиска
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> CheckClusterForKeywords(int id) 
        {
            using (var context = CreateDatabaseContext())
            {
                if (await context.searchClusters.Where(p => p.ClusterId == id).FirstOrDefaultAsync() != null)
                {
                    return true; 
                }
                else 
                {
                    return false;
                }
            }
        }

        public async Task<List<int>> СompleteМatch(string keyWord) 
        {
            using (var context = CreateDatabaseContext())
            {
                     return await context.searchClusters.Where(p => p.KeyWord == keyWord)
                    .Select( p =>  p.ClusterId)
                    //.Select( p => new { p.Id,p.ClusterId})
                    .Distinct()
                    .ToListAsync();
            }
        }

        
    }
}
