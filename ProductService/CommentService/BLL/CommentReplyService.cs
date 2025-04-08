using BLL.Dto;
using BLL.Dto.ReplyComment;
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
    public class CommentReplyService : ICommentReplyService
    {

        private readonly CommentReplyRepository _commentReplyRepository;
        public CommentReplyService(IContextManager contextManager) => _commentReplyRepository = new CommentReplyRepository(contextManager);


        public async Task<AnswerWithBackendDto<ReplyCommentDto>> AddReplyComment(AddReplyCommentDto addReplyCommentDto)
        {
            AnswerWithBackendDto<ReplyCommentDto> result = new();
            var item = await _commentReplyRepository.Add(addReplyCommentDto.CommentUserId, addReplyCommentDto.TextComment);
            if (item == null)
            {
                result.AddErrorLog("Ошибка не удалось создать комментарий");
            }
            else 
            {
                result.AddObject(Adapters.ReplyCommentAdapter.ConvertToCommentDTO(item));
                return result;
            }
           // return (await _commentReplyRepository.Add(addReplyCommentDto.CommentUserId, addReplyCommentDto.TextComment)).IdComment;

        }

        public async Task<bool> DeleteCommentReply(DeleteCommentReplyDto Dto)
        {
            CommentReply? commentReply = await _commentReplyRepository.Get(Dto.Id);
            if (commentReply is not null)
            {
                commentReply.IsDeleted = true;
                await _commentReplyRepository.Update(commentReply);
                return true;// $"Комментарий удалён {commentReply.ClusterId}";
            }
            else
            {
                return false;// "Не удалось удалить комментарий ввиду отсутствия";
            }
        }

        public async Task<bool> UpdateCommentReply(UpdateCommentReplyDto updateCommentReplyDto)
        {
            CommentReply commentReply = await _commentReplyRepository.Get(updateCommentReplyDto.IdCommentReply);
            commentReply.Text = updateCommentReplyDto.TextComment;
            if (commentReply != null)
            {
                string newTextComment = (await _commentReplyRepository.Update(commentReply)).Text;
                if (newTextComment == updateCommentReplyDto.TextComment)
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
