using CommentService.Domain;
using CommentService.DAL;
using SupperBackEnd.Dto;

namespace CommentService.BLL
{
    public class CommentMainService : ICommentMainService
    {
        private readonly CommentRepository _commentRepository;
        private readonly CommentReplyRepository _commentReplyRepository;
        private readonly RatingRepository _ratingRepository;


        public CommentMainService(IContextManager contextManager) 
        {
            _commentRepository = new CommentRepository(contextManager);
            _commentReplyRepository = new CommentReplyRepository(contextManager);
            _ratingRepository = new RatingRepository(contextManager);
        }


        public async Task<AnswerWithBackendDto<CommentDto>> AddNewComment(AddCommentDto commentDto)
        {
            AnswerWithBackendDto<CommentDto> answerWithBackendDto = new();
            Domain.Comment comment = CommentAdapter.ConvertToEntity(commentDto);
            if (await _commentRepository.GetCommentUser(comment.UserId, comment.ProductId) is null)
            {
                CommentReply reply = new();
                var result = await _commentRepository.Add(comment, reply);
                await AddReting(commentDto.ProductId, commentDto.Estimation);
                answerWithBackendDto.AddObject(CommentAdapter.ConvertToCommentDTO(comment));
                return answerWithBackendDto;// Комментарий создан {comment.ClusterId}
            }
            else
            {
                answerWithBackendDto.AddErrorLog("Не удалось создать новый комментарий комментарий ввиду уже существующего");
                return answerWithBackendDto; // 
            }
        }

        public async Task<AnswerWithBackendDto<CommentDto>> DeleteComment(DeleteCommentDto deleteCommentDto)
        {
            AnswerWithBackendDto<CommentDto> answerWithBackendDto = new();
            Domain.Comment? comment = await _commentRepository.Get(deleteCommentDto.Id);
            if (comment is not null && comment.IsDeleted == false)
            {
                CommentReply commentReply = await _commentReplyRepository.Get(comment.IdReply);
                int productId = comment.ProductId;
                decimal reting = comment.Estimation;
                comment.IsDeleted = true;
                commentReply.IsDeleted = true;
                await _commentRepository.Update(comment);
                await _commentReplyRepository.Update(commentReply);
                await DeleteReting(productId, reting);
                answerWithBackendDto.DataReceived = true;
                return answerWithBackendDto;
            }
            else
            {
                answerWithBackendDto.AddErrorLog("Произошла ошибка. Удаления комментария не возможно!");
                return answerWithBackendDto;
            }
        }


        public async Task<AnswerWithBackendDto<CommentDto>> GetCommentProduct(GetAllCommentsProduct getAllCommentsProduct) 
        {
            AnswerWithBackendDto<CommentDto> answerWithBackendDto = new();
            var items = await _commentRepository.GetAllCommentOnTheProduct(getAllCommentsProduct.IdProduct);
            if (items.Count == 0) 
            {
                answerWithBackendDto.AddErrorLog("По указаному запросу ничего не найдено");
            }
            answerWithBackendDto.AddObject(CommentAdapter.ConvertToCommentDTO(items));
            return answerWithBackendDto;
        }
            

        public async Task<AnswerWithBackendDto<CommentDto>> UpdateComment(UpdateCommentDto updateCommentDto)
        {
            AnswerWithBackendDto<CommentDto> answerWithBackendDto = new();
            Domain.Comment? comment = await _commentRepository.Get(updateCommentDto.CommentId);
            if (comment is not null)
            {
                int productId = comment.ProductId;
                decimal newReting = comment.Estimation;
                decimal OldReting = updateCommentDto.Estimation;

                decimal oldEstimation = comment.Estimation;
                comment.Text = updateCommentDto.TextComment;
                comment.Estimation = updateCommentDto.Estimation;

                var result = await _commentRepository.Update(comment);
                await UpdateReting(productId, newReting, OldReting);
                answerWithBackendDto.AddObject(CommentAdapter.ConvertToCommentDTO( result));
                return answerWithBackendDto;
            }
            else
            {
                answerWithBackendDto.AddErrorLog("Не удалось обновить комментарий из-за отсутствия его в бд");
                return answerWithBackendDto;// "Не удалось обновить комментарий из-за отсутствия его в бд";
            }
        }

        public async Task<bool> AddReting(int productId, decimal reting)
        {
            Rating rating = await _ratingRepository.Get(productId);
            if (rating is null)
            {
                rating = new Rating();
                rating.AverageRating = reting;
                rating.AmountOfComments = 1;
                await _ratingRepository.Add(rating);
                return true;
            }
            else
            {
                rating.AverageRating = (rating.AverageRating * rating.AmountOfComments + rating.AverageRating) / rating.AmountOfComments + 1;
                // востанавливаем рейтинг и прибавляем новые данные, потом делем на количество отзывом
                rating.AmountOfComments = rating.AmountOfComments + 1;
                await _ratingRepository.Update(rating);
                return true;
            }
        }
        public async Task<bool> DeleteReting(int productId, decimal reting)
        {

            Rating rating = await _ratingRepository.Get(productId);

            if (rating.AmountOfComments == 0)
            {
                rating.AverageRating = reting;
                rating.AmountOfComments = 1;
                await _ratingRepository.Update(rating);
                return true;
            }
            else
            {
                rating.AverageRating = (rating.AverageRating * rating.AmountOfComments - reting) / rating.AmountOfComments - 1;
                // востанавливаем рейтинг и прибавляем новые данные, потом делем на количество отзывом
                rating.AmountOfComments = rating.AmountOfComments - 1;
                await _ratingRepository.Update(rating);
                return true;
            }
        }

        public async Task<bool> UpdateReting(int productId, decimal newReting, decimal oldReting)
        {
            Rating  rating = await _ratingRepository.Get(productId);
            decimal reting = newReting - oldReting;
            if (rating == null)
            {
                return false;
            }
            if (rating.AmountOfComments == 1)
            {
                rating.AverageRating = newReting;
                rating.AmountOfComments = 1;
                await _ratingRepository.Update(rating);
                return true;
            }
            else
            {
                rating.AverageRating = (rating.AverageRating * rating.AmountOfComments + reting) / rating.AmountOfComments;
                // востанавливаем рейтинг и прибавляем новые данные, потом делем на количество отзывом
                rating.AmountOfComments = rating.AmountOfComments;
                await _ratingRepository.Update(rating);
                return true;
            }
        }
    }
}
