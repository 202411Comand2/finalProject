using BLL.Dto.Cluster;
using BLL.Dto.SearchCluster;

namespace BLL.Products.Abstractions
{
    public interface ISearchClusterService
    {
        /// <summary>
        /// Добавить кластер(классификатор) корневой элемент
        /// </summary>
        /// <param name="clusterDto">Название кластера</param>
        /// <returns>Успешно добавлен кластер</returns>
        public Task<bool> AddSearchClusterService(AddSearchClusterDto clusterDto);
        
    }
}
