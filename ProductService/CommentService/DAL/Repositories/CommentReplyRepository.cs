using DAL.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    
    public class CommentReplyRepository : BaseRepository<CommentReply>
    {
        public CommentReplyRepository(IContextManager manager) : base(manager)
        {

        }
        /// <summary>
        /// Обновить текст комментариев
        /// </summary>
        /// <param name="commentUserId"></param>
        /// <param name="textComment"></param>
        /// <returns></returns>
        public  async Task<CommentReply> Add(int commentUserId, string textComment)
        {
            using (var context = CreateDatabaseContext())
            {
                int commnent =  (await context.comments.FindAsync(commentUserId)).IdReply;
                var reply = await context.commentReplies.FindAsync(commnent);
                reply.Text=textComment;
                await context.SaveChangesAsync();
                return reply;
            }
        }
    }
}
