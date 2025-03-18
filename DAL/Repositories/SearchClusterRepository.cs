using DAL.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
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
                await context.SearchClusters.AddRangeAsync(tagEntities); // Пакетное добавление
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
                await context.SearchClusters
                   .Where(e => ids.Contains(e.Id))
                   .ExecuteDeleteAsync();
                return true;
            }
        }

    }
}
