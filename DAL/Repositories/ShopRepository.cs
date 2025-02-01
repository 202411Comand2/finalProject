using DAL.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ShopRepository : BaseRepository<Shop>
    {
        public ShopRepository(IContextManager manager) : base(manager)
        {

        } 
        /// <summary>
          /// Зарегистрировать владельца магазина
          /// </summary>
          /// <param name="entity"></param>
          /// <returns></returns>
        public override Task<Shop> Add(Shop entity)
        {
            return base.Add(entity);
        }


        /// <summary>
        /// Проверить имя магазина на существование
        /// </summary>
        /// <param name="name">Имя магазина</param>
        /// <returns>false - если магазина нет, true - если магазин есть в бд</returns>
        public async Task<bool> CheckNameShop(string name)
        {
            using (var context = CreateDatabaseContext())
            {
                var ob = await context.Shops
                    .FirstOrDefaultAsync(p => p.Name == name);
                if (ob == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }return true;
        }
    }
}
