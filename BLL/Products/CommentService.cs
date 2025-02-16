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

namespace BLL.Products
{
    public class CommentService : ICommentService
    {
        private readonly CommentRepository _commentRepository;
        private readonly CommentReplyRepository _commentReplyRepository;

        public CommentService(IContextManager contextManager) 
        {
            _commentRepository = new CommentRepository(contextManager);
            _commentReplyRepository = new CommentReplyRepository(contextManager);
        }


        public async Task<bool> AddNewComment(int userId, int shopId, int productId, string textComment, decimal Estimation)
        {
            Comment? comment = await _commentRepository.GetCommentUser(userId, productId);
            //if (await _shopRepository.Get(idShop) is null)
            //{
            //    return "Ошибка. Отсутствует магазин!";
            //}
            //if (await _productRepository.Get(idProduct) is null)
            //{
            //    return "Ошибка. Отсутствует продукт!";
            //}
            //if (await _userRepository.Get(idUser) is null)
            //{
            //    return "Ошибка. Отсутствует пользователь!";
            //}
            // Domain.Entities.Product product = await _productRepository.Get(idProduct);
            
            if (comment is null)
            {
                comment = new Comment
                {
                    Estimation = Estimation,
                    Text = textComment,
                    UserId = userId,
                    ShopId = shopId,
                    IdProduct = productId,
                };
                CommentReply reply = new();
                var result = await _commentRepository.Add(comment, reply);
               // await UpdateRatingProduct(comment, "create", 0);
                return true;// Комментарий создан {comment.Id}
            }
            else
            {
                return false; // Не удалось создать новый комментарий комментарий ввиду наличия
            }
        }

        public async Task<bool> DeleteComment(int CommentId)
        {
            Comment? comment = await _commentRepository.Get(CommentId);
            if (comment is not null && comment.IsDeleted == false)
            {
                CommentReply commentReply = await _commentReplyRepository.Get(comment.IdReply);
                comment.IsDeleted = true;
                commentReply.IsDeleted = true;
                await _commentRepository.Update(comment);
                await _commentReplyRepository.Update(commentReply);
               // await UpdateRatingProduct(comment, "delete", 0);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Comment>> GetCommentProduct(int productId) => await _commentRepository.GetAllCommentOnTheProduct(productId);
            

        public async Task<bool> UpdateComment(int CommentId, string textComment, decimal Estimation)
        {
            Comment? comment = await _commentRepository.Get(CommentId);
            if (comment is not null)
            {
                decimal oldEstimation = comment.Estimation;
                comment.Text = textComment;
                comment.Estimation = Estimation;

                var result = await _commentRepository.Update(comment);
                //   await UpdateRatingProduct(comment, "update", oldEstimation);
                return true;// $"Комментарий обновлён {comment.Id}";
            }
            else
            {
                return false;// "Не удалось обновить комментарий из-за отсутствия его в бд";
            }
        }
    }
}
