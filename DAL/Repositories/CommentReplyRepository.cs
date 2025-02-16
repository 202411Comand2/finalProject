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
        public  async Task<bool> Add(int commentUserId, string textComment)
        {
            using (var context = CreateDatabaseContext())
            {
                int commnent =  (await context.Comments.FindAsync(commentUserId)).IdReply;
                var reply = await context.CommentReply.FindAsync(commnent);
                reply.Text=textComment;
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
