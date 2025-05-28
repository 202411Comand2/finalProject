using BLL.Dto.Comment;
using Domain.Entities;

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
        public static Comment ConvertToEntity(AddCommentDto сommentDto)
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
