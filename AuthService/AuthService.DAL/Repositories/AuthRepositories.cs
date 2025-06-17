using Microsoft.EntityFrameworkCore;
using AuthService.Domain.Entities;

namespace AuthService.DAL
{
    public class AuthRepositories : BaseRepository<User>
    {
        public AuthRepositories(IContextManager manager) : base(manager)
        {

        }

        /// <summary>
        /// Поиск пользователя в бд по nickname
        /// </summary>
        /// <param name="nickName"></param>
        /// <returns></returns>
        public async Task<User> SearchUserNickName(string login)
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.Users.Where(p => p.Login == login).FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// Получить пользователя по login и password, который вводит пользователь при авторизации
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> GetUserLoginPassword(string login, string password) 
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.Users
                    .Where(l => l.Login == login && l.Password == password)
                    .FirstOrDefaultAsync();
            }
        }




        ///// <summary>
        ///// Получить продукты по магазину
        ///// </summary>
        ///// <param name="shopId">Id магазина</param>
        ///// <returns></returns>
        //public async Task<List<Product>> GetShopProducts(int shopId) 
        //{
        //    using (var context = CreateDatabaseContext())
        //    {
              
        //        return await context.Products.Where(p => p.ShopId == shopId).ToListAsync();
        //    }
        //}

        ///// <summary>
        ///// Вернуть список продуктов по кластреру
        ///// </summary>
        ///// <param name="clusterId">Id Кластера</param>
        ///// <returns></returns>
        //public async Task<List<Product>> GetProductsByCluster(int clusterId) 
        //{
        //    using (var context = CreateDatabaseContext())
        //    { 
        //        return await context.Products.Where(p => p.ClusterId == clusterId).ToListAsync();
        //    }
        //}
        ///// <summary>
        ///// Вернуть продукты по кластерам
        ///// </summary>
        ///// <param name="idsCluster"></param>
        ///// <returns></returns>
        //public async Task<List<Product>> GetProductsByClusters(List<int> idsCluster) 
        //{
        //    using (var context = CreateDatabaseContext())
        //    {
        //        return await context.Products
        //        .Where(e => idsCluster.Contains(e.Id)) 
        //        .ToListAsync();
        //    }
        //}


        ///// <summary>
        ///// Получить продукт со связими (one to one)
        ///// </summary>
        ///// <param name="idProduct">id продукта</param>
        ///// <returns></returns>
        //public async Task<Product> GetWithInclude(int idProduct)
        //{
        //    using (var context = CreateDatabaseContext())
        //    {
        //        return await context.Products
        //            .Include(cl => cl.ClusterID)
        //            .Include(sh => sh.ShopId)
        //            .Where(p => p.Id == idProduct).FirstOrDefaultAsync();
        //    }
        //}

    }
}
