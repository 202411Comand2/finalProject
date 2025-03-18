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

        /// <summary>
        /// Удалить ключевые слова кластеры
        /// </summary>
        /// <param name="deleteSearchClusterDto"></param>
        /// <returns></returns>
        public Task<bool> DeleteSearchClusterService(List<DeleteSearchClusterDto> deleteSearchClusterDto);



    }
}
