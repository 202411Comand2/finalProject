using SupperBackEnd.Dto;

namespace ClusterService.BLL
{
    public interface IClusterMainService
    {

        /// <summary>
        /// Добавить кластер(классификатор) корневой элемент
        /// </summary>
        /// <param name="clusterDto">Название кластера</param>
        /// <returns>Успешно добавлен кластер</returns>
        public Task<AnswerWithBackendDto<ClusterDto>> AddNewCluster(AddClusterDto clusterDto);


        /// <summary>
        /// Обновить имя кластер
        /// </summary>
        /// <param name="clasterId">id кластера</param>
        /// <param name="newNameClaster">Новое имя кластера</param>
        /// <returns></returns>
        public Task<AnswerWithBackendDto<ClusterDto>> UpdateNameCluster(UpdateCluseterDto updateCluseterDto);


        /// <summary>
        /// Удаление кластера
        /// </summary>
        /// <param name="clusterId">id кластера</param>
        /// <returns></returns>
        public Task<AnswerWithBackendDto<ClusterDto>> DeleteCluster(DeleteClusterDto deleteClusterDto);



        /// <summary>
        /// Получить все элементы кластеров
        /// </summary>
        /// <returns>Коллекцию кластеров</returns>
        public Task<AnswerWithBackendDto<ClusterDto>> GetAllElementsCluster();

        /// <summary>
        /// Получить только корневые элементы кластера
        /// </summary>
        public Task<AnswerWithBackendDto<ClusterDto>> GetRootElementsCluster();


        ////TODO что делать ошибку кидать или возвращаться null
        /// <summary>
        /// Получить дочерние элементы кластера
        /// </summary>
        /// <param name="clusterId">ClusterId кластера</param>
        /// <returns></returns>
        public Task<AnswerWithBackendDto<ClusterDto>> GetChildrenElementsCluster(GetClusterDto getClusterDto);

        /// <summary>
        /// Получить по id родителя всё кластера
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<AnswerWithBackendDto<ClusterDto>> GetChildrenElementsCluster(int id);

          /// <summary>
          /// Поиск по ключеву слову кластеров
          /// </summary>
          /// <param name="keyWord"></param>
          /// <returns></returns>
        public Task<AnswerWithBackendDto<ClusterDto>> SerchCluster(string keyWord);
    }
}