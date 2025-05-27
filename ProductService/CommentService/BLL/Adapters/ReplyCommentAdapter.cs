using BLL.Dto.ReplyComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace BLL.Adapters
{
    public class ReplyCommentAdapter
    {

        /// <summary>
        /// Преобразовать из Entitie ReplyComment  в  ReplyCommentDto
        /// </summary>
        /// <param name="comment">Комментарий Entitie Comment</param>
        /// <returns>Comment</returns>
        public static ReplyCommentDto ConvertToCommentDTO(CommentReply comment)
        {
            return new ReplyCommentDto()
            {
                Id = comment.Id,
                IsDeleted = comment.IsDeleted,
                Text = comment.Text,
            };
        }
    }
}
