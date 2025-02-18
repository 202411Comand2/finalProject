using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Products.Abstractions
{
    public interface IFavoriteProductService
    {

        /// <summary>
        /// Добавить товар в избранное
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="productId">Id продукта</param>
        /// <returns></returns>
        public Task<bool> AddFavoriteProduct(int userId, int productId);

        /// <summary>
        /// Удалить товар из избранного
        /// </summary>
        /// <param name="davoriteId">Id избранной позиции</param>
        /// <returns></returns>
        public Task<bool> DeleteFavoriteProduct(int favoriteId);
       

    }
}
