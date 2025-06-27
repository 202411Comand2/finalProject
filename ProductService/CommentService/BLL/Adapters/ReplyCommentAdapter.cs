using CommentService.Domain;

namespace CommentService.BLL
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
