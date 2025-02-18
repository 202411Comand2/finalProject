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
        public Task<List<Comment>> GetCommentProduct(int productId);


        ////TODO  как проверить, что пользователь купил товар и что он на него может оставить отзыв?
        /// <summary>
        /// Добавить отзыв на товар
        /// </summary>
        /// <param name="userId">id пользователя</param>
        /// <param name="shopId">id магазина</param>
        /// <param name="productId">id продукта</param>
        /// <param name="textComment">Текст комментария</param>
        /// <param name="estimation">Оценка</param>
        /// <returns></returns>
        public Task<bool> AddNewComment(int userId, int shopId, int productId, string textComment, decimal estimation);


        /// <summary>
        /// Обновление комментария
        /// </summary>
        /// <param name="сommentId">id комментария</param>
        /// <param name="textComment">Новый текст комментария</param>
        /// <param name="Estimation">Новая оценка комментария</param>
        /// <returns></returns>
        public Task<bool> UpdateComment(int сommentId, string textComment, decimal Estimation);


        /// <summary>
        /// Удаление комментарий (скрыть IsDeleted = true)
        /// </summary>
        /// <param name="сommentId">id комментария</param>
        /// <returns></returns>
        public Task<bool> DeleteComment(int сommentId);

    }
}
