using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Domain.Entities.Product;

namespace BLL.Products.Abstractions
{
    public interface IProductService
    {
        /// <summary>
        /// Добавить продукт в асортимент магазина
        /// </summary>
        /// <param name="shopId">Id магазина</param>
        /// <param name="clusterId">Id кластера</param>
        /// <param name="nameProduct">Название продукта</param>
        /// <param name="description">Описание продукта</param>
        /// <param name="price">Цена товара</param>
        /// <param name="barcode">штрихкод</param>
        /// <param name="modelNumber">Номер модели</param>
        /// <returns>Удалось добавить продукт</returns>
        public Task<bool> AddProduct(int shopId, int clusterId,string nameProduct, string description, decimal price, int barcode, string modelNumber);

        /// <summary>
        /// Обновить продукт 
        /// </summary>
        /// <param name="productId">Id Продукта</param>
        /// <param name="clusterId">Id кластера</param>
        /// <param name="nameProduct">Название продукта</param>
        /// <param name="description">Описание продукта</param>
        /// <param name="price">Цена товара</param>
        /// <param name="barcode">штрихкод</param>
        /// <param name="modelNumber">Номер модели</param>
        /// <returns>Удалось добавить продукт</returns>
        public Task<bool> UpdateProduct(int productId, int clusterId, string nameProduct, string description, decimal price, int barcode, string modelNumber);

        /// <summary>
        /// Удаление продукта
        /// </summary>
        /// <param name="productId">id продукта</param>
        /// <returns>Удалось ли удалить продукт</returns>
        public Task<bool> DeleteProduct(int productId);

        /// <summary>
        /// Получить все продукты магазина
        /// </summary>
        /// <param name="shopId">Id магазина</param>
        /// <returns>Коллекция продуктов по указаному продукту</returns>
        public Task<List<Domain.Entities.Product>> GetShopProducts(int shopId);

        /// <summary>
        /// Получить всё продукты
        /// </summary>
        /// <param name="shopId">Id магазина</param>
        /// <returns>Коллекция продуктов</returns>
        public Task<List<Domain.Entities.Product>> GetAllProduct();

    }
}
