using CommentService.Domain;
using Microsoft.EntityFrameworkCore;

namespace CommentService.DAL
{
    public class CommentRepository : BaseRepository<Domain.Comment>
    {
        public CommentRepository(IContextManager manager) : base(manager)
        {

        }
        
        /// <summary>
        /// Получить все комментарии по продукту
        /// </summary>
        /// <param name="idProduct">id продукта по которому нужно получить все комментарии пользователей</param>
        /// <returns></returns>
        public async Task<List<Domain.Comment>> GetAllCommentOnTheProduct(int idProduct)
        {
            using (var context = CreateDatabaseContext())
            {
               return await context.comments.Where(p => p.ProductId == idProduct ).ToListAsync();
            }
        }

        /// <summary>
        /// Получить объект со связими
        /// </summary>
        /// <param name="idComment">id Комментария</param>
        /// <returns></returns>
        //public async Task<Comment> GetWithInclude(int idComment) 
        //{
        //    using (var context = CreateDatabaseContext())
        //    {
        //        return await context.Comments
        //            .Include(c=>c.Product)
        //            .Include(d=>d.Replies)
        //            .Include(u=>u.User)
        //            .Include(s=>s.Shop)
        //            .Where(p => p.Id == idComment).FirstOrDefaultAsync();
        //    }
        //}



        /// <summary>
        /// Вернуть отзыв на товар пользователя
        /// </summary>
        /// <param name="idUser">ID пользователя</param>
        /// <param name="idProduct">ID комментария</param>
        /// <returns>Возвращает отзыв пользователя</returns>
        public async Task<Domain.Comment?> GetCommentUser(int idUser, int idProduct)
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.comments.FirstOrDefaultAsync(p => p.UserId == idUser && p.ProductId == idProduct);
            }
        }


        /// <summary>
        /// Добавление комментария
        /// </summary>
        /// <param name="comment">Объект комментарий</param>
        /// <returns></returns>
        public  async Task<Domain.Comment> Add(Domain.Comment comment , CommentReply reply) 
        {
            using (var context = CreateDatabaseContext())
            {
                comment.Replies = reply;
                //comment.UserName = (await context.Users.FindAsync(comment.UserId))?.Name;
                await context.comments.AddAsync(comment);
                await context.SaveChangesAsync();

                reply.IdComment = comment.Id;
                await context.SaveChangesAsync();
            }
            return comment;
        }



    }
}
