using DAL.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    
    public class FavoriteRepository : BaseRepository<Favorite>
    {
        public FavoriteRepository(IContextManager manager) : base(manager)
        {

        }

        /// <summary>
        /// Вернуть избранную позицию пользователя
        /// </summary>
        /// <param name="idUser">ID пользователя</param>
        /// <param name="idProduct">ID продукта</param>
        /// <returns>Возвращает избранную позицию пользователя</returns>
        public async Task<Favorite?> GetFavoriteUser(int idUser, int idProduct)
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.favorites.FirstOrDefaultAsync(p => p.UserId == idUser && p.IdProduct == idProduct);
            }
        }

        /// <summary>
        /// Получить избранные позиции пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<List<Favorite>> GetFavoritesUser(int UserId) 
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.favorites.Where(p => p.UserId == UserId).ToListAsync();
            }
        }
    }
}
