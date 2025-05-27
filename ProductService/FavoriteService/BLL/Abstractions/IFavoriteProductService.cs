using BLL.Dto;
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
        public Task<AnswerWithBackendDto<FavoriteDto>> AddFavoriteProduct(AddFavoriteDto addFavoriteDto);

        /// <summary>
        /// Удалить товар из избранного
        /// </summary>
        /// <param name="davoriteId">ClusterId избранной позиции</param>
        /// <returns></returns>
        public Task<AnswerWithBackendDto<FavoriteDto>> DeleteFavoriteProduct(DeleteFavoriteDto Dto);

        /// <summary>
        /// Получить список избранных позиции пользователя
        /// </summary>
        /// <param name="getFavoriteDto"></param>
        /// <returns></returns>
        public Task<AnswerWithBackendDto<FavoriteDto>> GetFavoriteUser(GetFavoriteDto getFavoriteDto);
        

    }
}
