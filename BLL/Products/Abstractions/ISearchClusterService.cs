using BLL.Dto.Cluster;
using BLL.Dto.SearchCluster;
using Domain.Entities;

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
        public Task<bool> DeleteSearchClusterService(DeleteSearchClusterDto deleteSearchClusterDto);

        /// <summary>
        /// Обновление категории ключевого слова
        /// </summary>
        /// <param name="updateSearchClusterDto"></param>
        /// <returns></returns>
        public Task<bool> UpdateSearchClusterService(UpdateSearchClusterDto updateSearchClusterDto);

        /// <summary>
        /// Получить всё ключевые слова по которым идёт поиск у кластера
        /// </summary>
        /// <param name="getSearchClusterDto"></param>
        /// <returns></returns>
        public Task<GetSearchClusterDto> GetSearchClusterId(int id);

        public Task<(List<Cluster>, List<int>)> SearchProducts(string keyWord);
    }
}
