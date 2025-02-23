using BLL.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Adapters
{
    /// <summary>
    /// Класс адаптер, который служит для преобразования комментариев в DTO и Entitie
    /// </summary>
    public class CommentAdapter
    {
        /// <summary>
        /// Преобразовать из Entitie Comment в CommentDto
        /// </summary>
        /// <param name="comment">Комментарий Entitie</param>
        /// <returns>CommentDto</returns>
        public static CommentDto ConvertToDTOComment(Comment comment)
        {
            return new CommentDto()
            {
                UserId = comment.UserId,
                ShopId = comment.ShopId,
                ProductId = comment.IdProduct,
                TextComment = comment.Text,
                Estimation = comment.Estimation,
            };
        }
     
        /// <summary>
        /// Преобразовать из CommentDto в Entitie Comment
        /// </summary>
        /// <param name="comment">Комментарий Entitie</param>
        /// <returns>Comment</returns>
        public static Comment ConvertToEntity(CommentDto сommentDto)
        {
            return new Comment
            {
                UserId = сommentDto.UserId,
                ShopId = сommentDto.ShopId,
                IdProduct = сommentDto.ProductId,
                Text = сommentDto.TextComment,
                Estimation = сommentDto.Estimation,
            };
        }
    }
}
