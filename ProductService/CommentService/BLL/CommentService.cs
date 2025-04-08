using BLL.Products.Abstractions;
using DAL.Abstractions;
using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BLL.Dto.Comment;
using BLL.Dto;

namespace BLL.Products
{
    public class CommentService : ICommentService
    {
        private readonly CommentRepository _commentRepository;
        private readonly CommentReplyRepository _commentReplyRepository;
        private readonly RatingRepository _ratingRepository;


        public CommentService(IContextManager contextManager) 
        {
            _commentRepository = new CommentRepository(contextManager);
            _commentReplyRepository = new CommentReplyRepository(contextManager);
            _ratingRepository = new RatingRepository(contextManager);
        }


        public async Task<AnswerWithBackendDto<CommentDto>> AddNewComment(AddCommentDto commentDto)
        {
            AnswerWithBackendDto<CommentDto> answerWithBackendDto = new();
            Comment comment = Adapters.CommentAdapter.ConvertToEntity(commentDto);
            if (await _commentRepository.GetCommentUser(comment.UserId, comment.IdProduct) is null)
            {
                CommentReply reply = new();
                var result = await _commentRepository.Add(comment, reply);
                await AddReting(commentDto.ProductId, commentDto.Estimation);
                answerWithBackendDto.AddObject(Adapters.CommentAdapter.ConvertToCommentDTO(comment));
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
            Comment? comment = await _commentRepository.Get(deleteCommentDto.Id);
            if (comment is not null && comment.IsDeleted == false)
            {
                CommentReply commentReply = await _commentReplyRepository.Get(comment.IdReply);
                int productId = comment.IdProduct;
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
            answerWithBackendDto.AddObject(Adapters.CommentAdapter.ConvertToCommentDTO(items));
            return answerWithBackendDto;
        }
            

        public async Task<AnswerWithBackendDto<CommentDto>> UpdateComment(UpdateCommentDto updateCommentDto)
        {
            AnswerWithBackendDto<CommentDto> answerWithBackendDto = new();
            Comment? comment = await _commentRepository.Get(updateCommentDto.CommentId);
            if (comment is not null)
            {
                int productId = comment.IdProduct;
                decimal newReting = comment.Estimation;
                decimal OldReting = updateCommentDto.Estimation;

                decimal oldEstimation = comment.Estimation;
                comment.Text = updateCommentDto.TextComment;
                comment.Estimation = updateCommentDto.Estimation;

                var result = await _commentRepository.Update(comment);
                await UpdateReting(productId, newReting, OldReting);
                answerWithBackendDto.AddObject(Adapters.CommentAdapter.ConvertToCommentDTO( result));
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
