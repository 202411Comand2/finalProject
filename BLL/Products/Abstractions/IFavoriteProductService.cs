using BLL.Dto.Favorite;
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
        /// <param name="userId">ClusterId пользователя</param>
        /// <param name="productId">ClusterId продукта</param>
        /// <returns></returns>
        public Task<bool> AddFavoriteProduct(AddFavoriteDto addFavoriteDto);

        /// <summary>
        /// Удалить товар из избранного
        /// </summary>
        /// <param name="davoriteId">ClusterId избранной позиции</param>
        /// <returns></returns>
        public Task<bool> DeleteFavoriteProduct(DeleteFavoriteDto Dto);
       

    }
}
