using BLL.ProductService;

namespace Test
{
    public interface IBLLShopServiceTest
    {
        #region работа с магазином

        /// <summary>
        /// Создание Владельца магазина
        /// </summary>
        /// <returns></returns>
        public Task<bool> CreateShop();

        /// <summary>
        /// Удаление магазина
        /// </summary>
        /// <returns></returns>
        public Task TestDeleteShop();


        /// <summary>
        /// Изменить имя магазина
        /// </summary>
        /// <returns></returns>
        public Task TestUpdateNameShop();

        #endregion

        #region Работа с кластером

        /// <summary>
        /// Создание элемента классификатора 
        /// </summary>
        /// <returns></returns>
        public Task TestAddCluster();

        /// <summary>
        /// Обновление кластера
        /// </summary>
        /// <returns></returns>
        public Task TestUpdateCluster();

        /// <summary>
        /// Удаление кластера
        /// </summary>
        /// <returns></returns>
        public Task TestDeleteCluster();

        /// <summary>
        /// Обновить позицию кластера в классификаторе
        /// </summary>
        /// <returns></returns>
        public Task TestUpdatePositionCluster();

        /// <summary>
        /// Получить кластеры по определённым условиям
        /// </summary>
        /// <returns></returns>
        public Task TestGetCluster();

        #endregion

        #region работа с товарами магазина
        public Task TestCreateProduct();
        public Task TestDeleteProduct();
        public Task TestUpdateProduct();
        public Task GetShopProducts();
        public Task GetAllProduct();
        #endregion

        #region Работа с отзывами
        /// <summary>
        /// Добавить комментарий
        /// </summary>
        /// <returns></returns>
        public Task TestAddComment();

        /// <summary>
        /// Обновить комментарий
        /// </summary>
        /// <returns></returns>
        public Task TestUpdateComment();

        /// <summary>
        /// Удаление(скрыть) комментария пользователя
        /// </summary>
        /// <returns></returns>
        public Task TestDeleteComment();

        /// <summary>
        /// Добавить  ответ на комментарий пользователя со стороны магазина
        /// </summary>
        /// <returns></returns>
        public Task TestAddCommentReply();

        /// <summary>
        /// Удалить(скрыть) ответы на комментарии владельцев товара
        /// </summary>
        /// <returns></returns>
        public Task TestDeleteCommentReply();

        /// <summary>
        /// Получить все комментарии по товару
        /// </summary>
        /// <returns></returns>
        public Task GetAllComment();

        #endregion

        #region тестирование избранных позиция магазина
        /// <summary>
        /// Добовление позиции в избраное
        /// </summary>
        /// <returns></returns>
        public  Task TestAddFavoriteProduct();
     
        /// <summary>
        /// Удаление избраной позиции
        /// </summary>
        /// <returns></returns>
        public  Task TestDeleteFavoriteProduct();
       
        #endregion
    }
}