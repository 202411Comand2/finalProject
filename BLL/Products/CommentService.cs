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
using BLL.ProductService;

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


        public async Task<bool> AddNewComment(int userId, int shopId, int productId, string textComment, decimal estimation)
        {
            Comment? comment = await _commentRepository.GetCommentUser(userId, productId);
           
            if (comment is null)
            {
                comment = new Comment
                {
                    Estimation = estimation,
                    Text = textComment,
                    UserId = userId,
                    ShopId = shopId,
                    IdProduct = productId,
                };
                CommentReply reply = new();
                var result = await _commentRepository.Add(comment, reply);
                // await UpdateRatingProduct(comment, "create", 0);
                await AddReting(productId, estimation);
                return true;// Комментарий создан {comment.Id}
            }
            else
            {
                return false; // Не удалось создать новый комментарий комментарий ввиду наличия
            }
        }



        public async Task<bool> DeleteComment(int commentId)
        {
            Comment? comment = await _commentRepository.Get(commentId);
           

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
                return true;
            }
            else
            {
                return false;
            }
        }

     
        public async Task<List<Comment>> GetCommentProduct(int productId) => await _commentRepository.GetAllCommentOnTheProduct(productId);
            

        public async Task<bool> UpdateComment(int CommentId, string textComment, decimal estimation)
        {
            Comment? comment = await _commentRepository.Get(CommentId);
            if (comment is not null)
            {
                int productId = comment.IdProduct;
                decimal newReting = comment.Estimation;
                decimal OldReting = estimation;

                decimal oldEstimation = comment.Estimation;
                comment.Text = textComment;
                comment.Estimation = estimation;

                var result = await _commentRepository.Update(comment);
                await UpdateReting(productId, newReting, OldReting);
                //   await UpdateRatingProduct(comment, "update", oldEstimation);
                return true;// $"Комментарий обновлён {comment.Id}";
            }
            else
            {
                return false;// "Не удалось обновить комментарий из-за отсутствия его в бд";
            }
        }

        public async Task<bool> AddReting(int productId, decimal reting)
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
