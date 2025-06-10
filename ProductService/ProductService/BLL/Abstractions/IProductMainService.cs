using SupperBackEnd.Dto;

namespace ProductService.BLL
{
    public interface IProductMainService
    {
        /// <summary>
        /// Добавить продукт в асортимент магазина
        /// </summary>
        ///<param name="productDto">Объект продукт</param>
        /// <returns>Удалось добавить продукт</returns>
        public Task<AnswerWithBackendDto<ProductDto>> AddProduct(AddProductDto productDto);// int shopId, int clusterId,string nameProduct, string description, decimal price, int barcode, string modelNumber);

        /// <summary>
        /// Обновить продукт 
        /// </summary>
        ///<param name="productDto">Объект продукт</param>>
        /// <returns>Удалось добавить продукт</returns>
        public Task<AnswerWithBackendDto<ProductDto>> UpdateProduct(UpdateProductDto productDto);// int ProductId, int clusterId, string nameProduct, string description, decimal price, int barcode, string modelNumber);

        /// <summary>
        /// Удаление продукта
        /// </summary>
        /// <param name="productDto">Модель продукта, достаточно id продукта, который нужно удалить</param>
        /// <returns>Удалось ли удалить продукт</returns>
        public Task<AnswerWithBackendDto<ProductDto>> DeleteProduct(DeleteProductDto productDto);

        /// <summary>
        /// Получить все продукты магазина
        /// </summary>
        /// <param name="shopId">ClusterId магазина</param>
        /// <returns>Коллекция продуктов по указаному продукту</returns>
        public Task<AnswerWithBackendDto<ProductDto>> GetShopProducts(GetallShopProductsDto productDto);

        /// <summary>
        /// Получить всё продукты
        /// </summary>
        /// <returns>Коллекция продуктов</returns>
        public Task<AnswerWithBackendDto<ProductDto>> GetAllProduct();

        /// <summary>
        /// Получить список продуктов, которые прикреплены к кластеру по кластру
        /// </summary>
        /// <param name="clusterId">ClusterId кластера</param>
        /// <returns></returns>
        public Task<AnswerWithBackendDto<ProductDto>> GetProductsByCluster(GetAllClusterProductsDto productDto);

        /// <summary>
        /// Получить список продуктов, которые прикреплены к кластеру по кластрам
        /// </summary>
        /// <param name="ClusterIds"></param>
        /// <returns></returns>
        public Task<AnswerWithBackendDto<ProductDto>> GetProductsByCluster(List<int> ClusterIds);

        /// <summary>
        /// Получить информацию о продукте по id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<AnswerWithBackendDto<ProductDto>> GetProductById(int id);
    }
}
