using BLL.Products.Abstractions;
using DAL.Abstractions;
using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Products
{
    public class CommentReplyService : ICommentReply
    {

        private readonly CommentReplyRepository _commentReplyRepository;
        public CommentReplyService(IContextManager contextManager) => _commentReplyRepository = new CommentReplyRepository(contextManager);


        public async Task<bool> AddReplyComment(int commentUserId, string textComment)
        {
            if (await _commentReplyRepository.Add(commentUserId, textComment))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> DeleteCommentReply(int commentReplyId)
        {
            CommentReply? commentReply = await _commentReplyRepository.Get(commentReplyId);
            if (commentReply is not null)
            {
                commentReply.IsDeleted = true;
                await _commentReplyRepository.Update(commentReply);
                return true;// $"Комментарий удалён {commentReply.Id}";
            }
            else
            {
                return false;// "Не удалось удалить комментарий ввиду отсутствия";
            }
        }

        public async Task<bool> UpdateCommentReply(int IdCommentReply, string textComment)
        {
            CommentReply commentReply = await _commentReplyRepository.Get(IdCommentReply);
            commentReply.Text = textComment;
            if (commentReply != null)
            {
                string newTextComment = (await _commentReplyRepository.Update(commentReply)).Text;
                if (newTextComment == textComment)
                { //Проверяем, что коментарий был обновлён
                    return true;
                }
                return false;
            }
            else 
            {
                return false;   
            }
        }
    }
}
