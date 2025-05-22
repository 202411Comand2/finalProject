using BLL.Dto.Comment;
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
        
        public static CommentDto ConvertToCommentDTO(Comment comment)
        {
            return new CommentDto()
            {
                Id = comment.Id,
                UserId = comment.UserId,
                ShopId = comment.ShopId,
                ProductId = comment.ProductId,
                TextComment = comment.Text,
                Estimation = comment.Estimation,
            };
        }

        public static List<CommentDto> ConvertToCommentDTO(List<Comment> ItemComment)
        {
            List< CommentDto > itemsCommentDto = new List< CommentDto >();
            foreach (var comment in ItemComment) 
            {
                itemsCommentDto.Add(new CommentDto()
                {
                    Id = comment.Id,
                    UserId = comment.UserId,
                    UserName = comment.UserName,
                    ShopId = comment.ShopId,
                    ProductId = comment.ProductId,
                    TextComment = comment.Text,
                    Estimation = comment.Estimation,
                    
                });
            }
            return itemsCommentDto;
        }

        /// <summary>
        /// Преобразовать из CommentDto в Entitie Comment
        /// </summary>
        /// <param name="comment">Комментарий Entitie</param>
        /// <returns>Comment</returns>
        public static Comment ConvertToEntity(AddCommentDto сommentDto)
        {
            return new Comment
            {
                UserId = сommentDto.UserId,
                ShopId = сommentDto.ShopId,
                UserName = сommentDto.UserName,
                ProductId = сommentDto.ProductId,
                Text = сommentDto.TextComment,
                Estimation = сommentDto.Estimation,
            };
        }
    }
}
