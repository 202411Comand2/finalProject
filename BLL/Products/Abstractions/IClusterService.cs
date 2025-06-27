using BLL.Dto.Cluster;

namespace BLL.Products.Abstractions
{
    public interface IClusterService
    {

        /// <summary>
        /// Добавить кластер(классификатор) корневой элемент
        /// </summary>
        /// <param name="clusterDto">Название кластера</param>
        /// <returns>Успешно добавлен кластер</returns>
        public Task<int> AddNewCluster(AddClusterDto clusterDto);

        ///// <summary>
        ///// Добавить кластер
        ///// </summary>
        ///// <param name="nameClaster">Название кластера</param>
        ///// <param name="ParentId">ClusterId родителя (-1 корень)</param>
        ///// <returns>Успешно добавлен кластер</returns>
        //public Task<bool> AddNewCluster(string nameClaster, int ParentId);


        ///// <summary>
        ///// Добавить кластер(классификатор) вложенный
        ///// </summary>
        ///// <param name="nameClaster">Имя кластера</param>
        ///// <param name="NameParent">Имя кластера родителя (-1 корень)</param>
        ///// <returns></returns>
        //public Task<bool> AddNewCluster(string nameClaster, string NameParent);

        /// <summary>
        /// Обновить имя кластер
        /// </summary>
        /// <param name="clasterId">id кластера</param>
        /// <param name="newNameClaster">Новое имя кластера</param>
        /// <returns></returns>
        public Task<bool> UpdateNameCluster(UpdateCluseterDto updateCluseterDto);


        ///// <summary>
        ///// Обновить кластер
        ///// </summary>
        ///// <param name="oldNameClaster">Старое имя кластера</param>
        ///// <param name="newNameClaster">Новое имя кластера</param>
        ///// <returns></returns>
        //public Task<bool> UpdateNameCluster(string oldNameClaster, string newNameClaster);


        ////ToDO при удалении кластера нужно проверять какие продукты к ним прикреплены?
        /// <summary>
        /// Удаление кластера
        /// </summary>
        /// <param name="clusterId">id кластера</param>
        /// <returns></returns>
        public Task<bool> DeleteCluster(DeleteClusterDto deleteClusterDto);


        ///// <summary>
        ///// Удаление кластера
        ///// </summary>
        ///// <param name="nameCluster">Название кластера</param>
        ///// <returns></returns>
        //public Task<bool> DeleteCluster(string nameCluster);


        ///// <summary>
        /////  Изменение позиции кластера в иерархии
        ///// </summary>
        ///// <param name="clusterId">id кластера, позицию которого нужно поменять в классификаторе</param>
        ///// <param name="parentId">id родителя кластера, куда нужно вложить (-1 означает корень) </param>
        ///// <returns></returns>
        //public Task<bool> UpdatePositionCluster(int clusterId, int parentId);


        ///// <summary>
        /////  Изменение позиции кластера в иерархии
        ///// </summary>
        ///// <param name="nameCluster">Название кластера</param>
        ///// <param name="idParent">id родителя кластера, куда нужно вложить (-1 означает корень) </param>
        ///// <returns></returns>
        //public Task<bool> UpdatePositionCluster(string nameCluster, int parentId);


        /// <summary>
        /// Получить все элементы кластеров
        /// </summary>
        /// <returns>Коллекцию кластеров</returns>
        public Task<List<ClusterDto>> GetAllElementsCluster();

        /// <summary>
        /// Получить только корневые элементы кластера
        /// </summary>
        public Task<List<ClusterDto>> GetRootElementsCluster();


        ////TODO что делать ошибку кидать или возвращаться null
        /// <summary>
        /// Получить дочерние элементы кластера
        /// </summary>
        /// <param name="clusterId">ClusterId кластера</param>
        /// <returns></returns>
        public Task<List<ClusterDto>> GetChildrenElementsCluster(GetClusterDto getClusterDto);


        ///// <summary>
        ///// Получить дочерние элементы кластера
        ///// </summary>
        ///// <param name="nameCluster">Имя кластера</param>
        ///// <returns></returns>
        //public Task<List<ClusterDto>> GetChildrenElementsCluster(string nameCluster);



    }
}