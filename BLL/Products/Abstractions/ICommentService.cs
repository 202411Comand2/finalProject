using BLL.Dto.Comment;
using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Products.Abstractions
{
    public interface ICommentService
    {
        /// <summary>
        /// Получить все комментарии по продукту
        /// </summary>
        /// <param name="productId">id комментария</param>
        /// <returns></returns>
        public Task<List<CommentDto>> GetCommentProduct(GetAllCommentsProduct productId);


        ////TODO  как проверить, что пользователь купил товар и что он на него может оставить отзыв?
        /// <summary>
        /// Добавить отзыв на товар
        /// </summary>
        /// <param name="commentDto">Объект commentDto</param>
        /// <returns></returns>
        public Task<bool> AddNewComment(AddCommentDto commentDto);


        /// <summary>
        /// Обновление комментария
        /// </summary>
        /// <returns></returns>
        public Task<bool> UpdateComment(UpdateCommentDto updateCommentDto);


        /// <summary>
        /// Удаление комментарий (скрыть IsDeleted = true)
        /// </summary>
        /// <param name="сommentId">id комментария</param>
        /// <returns></returns>
        public Task<bool> DeleteComment(DeleteCommentDto сommentId);


        /// <summary>
        /// Добавить новый рейтинг
        /// </summary>
        /// <param name="productId">ClusterId продукта</param>
        /// <param name="reting">Рентинг товара</param>
        /// <returns></returns>
        public Task<bool> AddReting(int productId, decimal reting);

        /// <summary>
        /// Обновление рейтига
        /// </summary>
        /// <param name="productId">ClusterId продукта</param>
        /// <param name="newReting">Рентинг товара</param>
        /// <param name="oldReting">Старый рейтинг товара</param>
        /// <returns></returns>
        public Task<bool> UpdateReting(int productId, decimal newReting, decimal oldReting);


        /// <summary>
        /// Удаление рейтинга
        /// </summary>
        /// <param name="productId">ClusterId продукта</param>
        /// <param name="reting">Рентинг товара</param>
        /// <returns></returns>
        public Task<bool> DeleteReting(int productId, decimal reting);

    }
}
