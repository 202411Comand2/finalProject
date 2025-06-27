using SupperBackEnd.Dto;

namespace CommentService.BLL
{
    public interface ICommentReplyService
    {
        /// <summary>
        /// Добавить ответ на комментарий пользователя со стороны магазина (id комментария пользователя)
        /// </summary>
        /// <param name="commentUserId">id комментария пользователя</param>
        /// <param name="textComment">Текст комментария</param>
        /// <returns></returns>
        public Task<AnswerWithBackendDto<ReplyCommentDto>> AddReplyComment(AddReplyCommentDto addReplyCommentDto);


        /// <summary>
        /// Обновить ответ на комментарий пользователя со стороны магазина (id комментария ответного)
        /// </summary>
        /// <param name="IdCommentReply">id комментария ответа</param>
        /// <param name="textComment">Текст нового комментария</param>
        /// <returns></returns>
        public Task<AnswerWithBackendDto<ReplyCommentDto>> UpdateCommentReply(UpdateCommentReplyDto updateCommentReplyDto);



        /// <summary>
        /// Удалить (скрыть IsDeleted = true) ответ на комментарий пользователя со стороны магазина 
        /// </summary>
        /// <param name="commentReplyId">id коментария ответа</param>
        /// <returns></returns>
        public Task<AnswerWithBackendDto<ReplyCommentDto>> DeleteCommentReply(DeleteCommentReplyDto Dto);


    }
}
