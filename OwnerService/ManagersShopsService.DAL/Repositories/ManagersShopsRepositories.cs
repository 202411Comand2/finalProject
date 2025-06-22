using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using OwnerService.Domain.Entities;

namespace ManagersShopsService.DAL
{
    public class ManagersShopsRepositories : BaseRepository<ManagersShops>
    {
        public ManagersShopsRepositories(IContextManager manager) : base(manager)
        {

        }

        /// <summary>
        /// Упращённая версия поиска магазинов пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<ManagersShops>> GetShop(int id)
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.ManagersShops.Where(p => p.UserId == id).ToListAsync();
            }
        }
        /// <summary>
        /// Получить менеджеров магазина
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<ManagersShops>> GetManager(int id)
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.ManagersShops.Where(p => p.ShopId == id).ToListAsync();
            }
        }

        //public async Task<bool> DeleteManager(int idUser, int idShop) 
        //{
        //    using (var context = CreateDatabaseContext()) 
        //    {
        //        return  await context.ManagersShops.Where(p=>p.UserId==idUser && p.ShopId==idShop).ExecuteDeleteAsync();
        //    }
        //}

        ///// <summary>
        ///// Поиск пользователя в бд по nickname
        ///// </summary>
        ///// <param name="nickName"></param>
        ///// <returns></returns>
        //public async Task<User> SearchUserNickName(string login)
        //{
        //    using (var context = CreateDatabaseContext())
        //    {
        //        return await context.Users.Where(p => p.Login == login).FirstOrDefaultAsync();
        //    }
        //}

        ///// <summary>
        ///// Получить пользователя по login и password, который вводит пользователь при авторизации
        ///// </summary>
        ///// <param name="login"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        //public async Task<User> GetUserLoginPassword(string login, string password) 
        //{
        //    using (var context = CreateDatabaseContext())
        //    {
        //        return await context.Users
        //            .Where(l => l.Login == login && l.Password == password)
        //            .FirstOrDefaultAsync();
        //    }
        //}



    }
}
