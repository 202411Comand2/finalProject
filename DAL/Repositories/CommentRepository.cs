using DAL.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CommentRepository : BaseRepository<Comment>
    {
        public CommentRepository(IContextManager manager) : base(manager)
        {

        }
        /// <summary>
        /// Получить все комментарии по продукту
        /// </summary>
        /// <param name="idProduct">id продукта по которому нужно получить все комментарии пользователей</param>
        /// <returns></returns>
        public async Task<List<Comment>> GetAllCommentOnTheProduct(int idProduct)
        {
            using (var context = CreateDatabaseContext())
            {
               return await context.Comments.Where(p => p.IdProduct == idProduct ).ToListAsync();
            }
        }

        /// <summary>
        /// Вернуть отзыв на товар пользователя
        /// </summary>
        /// <param name="idUser">ID пользователя</param>
        /// <param name="idProduct">ID комментария</param>
        /// <returns>Возвращает отзыв пользователя</returns>
        public async Task<Comment?> GetCommentUser(int idUser, int idProduct)
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.Comments.FirstOrDefaultAsync(p => p.UserId == idUser && p.IdProduct == idProduct);
            }
        }
    }
}
