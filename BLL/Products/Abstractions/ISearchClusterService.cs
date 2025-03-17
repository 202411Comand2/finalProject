using BLL.Dto.Cluster;

namespace BLL.Products.Abstractions
{
    public interface ISearchClusterService
    {
        /// <summary>
        /// Добавить кластер(классификатор) корневой элемент
        /// </summary>
        /// <param name="clusterDto">Название кластера</param>
        /// <returns>Успешно добавлен кластер</returns>
        public Task<int> AddSearchClusterService(AddClusterDto clusterDto);
        
    }
}
