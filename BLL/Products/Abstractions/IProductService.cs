using BLL.Dto;
using Domain.Entities;
using Microsoft.AspNetCore.Components.Web;
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
        public Task<bool> AddProduct(ProductDto productDto);// int shopId, int clusterId,string nameProduct, string description, decimal price, int barcode, string modelNumber);

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
        public Task<bool> UpdateProduct(ProductDto productDto);// int productId, int clusterId, string nameProduct, string description, decimal price, int barcode, string modelNumber);

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
        /// <returns>Коллекция продуктов</returns>
        public Task<List<Domain.Entities.Product>> GetAllProduct();

        /// <summary>
        /// Получить список продуктов, которые прикреплены к кластеру по кластру
        /// </summary>
        /// <param name="clusterId">Id кластера</param>
        /// <returns></returns>
        public Task<List<Domain.Entities.Product>> GetProductsByCluster(int clusterId);

        ///// <summary>
        ///// Добавить новый рейтинг
        ///// </summary>
        ///// <param name="productId">Id продукта</param>
        ///// <param name="reting">Рентинг товара</param>
        ///// <returns></returns>
        //public Task<bool> AddReting(int productId, decimal reting);

        ///// <summary>
        ///// Обновление рейтига
        ///// </summary>
        ///// <param name="productId">Id продукта</param>
        ///// <param name="newReting">Рентинг товара</param>
        ///// <param name="oldReting">Старый рейтинг товара</param>
        ///// <returns></returns>
        //public Task<bool> UpdateReting(int productId, decimal newReting, decimal oldReting);


        ///// <summary>
        ///// Удаление рейтинга
        ///// </summary>
        ///// <param name="productId">Id продукта</param>
        ///// <param name="reting">Рентинг товара</param>
        ///// <returns></returns>
        //public Task<bool> DeleteReting(int productId, decimal reting);
    }
}
