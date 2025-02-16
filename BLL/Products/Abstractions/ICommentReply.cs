using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Products.Abstractions
{
    public interface ICommentReply
    {
        /// <summary>
        /// Добавить ответ на комментарий пользователя со стороны магазина (id комментария пользователя)
        /// </summary>
        /// <param name="commentUserId">id комментария пользователя</param>
        /// <param name="textComment">Текст комментария</param>
        /// <returns></returns>
        public Task<bool> AddReplyComment(int commentUserId, string textComment);


        /// <summary>
        /// Обновить ответ на комментарий пользователя со стороны магазина (id комментария ответного)
        /// </summary>
        /// <param name="IdCommentReply">id комментария ответа</param>
        /// <param name="textComment">Текст нового комментария</param>
        /// <returns></returns>
        public  Task<bool> UpdateCommentReply(int IdCommentReply, string textComment);



        /// <summary>
        /// Удалить (скрыть IsDeleted = true) ответ на комментарий пользователя со стороны магазина 
        /// </summary>
        /// <param name="commentReplyId">id коментария ответа</param>
        /// <returns></returns>
        public Task<bool> DeleteCommentReply(int commentReplyId);


    }
}
