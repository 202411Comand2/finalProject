using DAL.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

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


        //TODO так как микросервисная архитектура, то нельзя вызывать из одного репозитория другой
        /// <summary>
        /// Удаление магазина и его асортимента
        /// </summary>
        /// <param name="shop">Экземпляр магазина</param>
        /// <returns></returns>
        public async Task<bool> DeleteShopWithProducts(Shop shop) 
        {
            return false;
            //using (var context = CreateDatabaseContext())
            //{ 
            //    shop.IsDelete = true;
            //    List<Product> shops = await context.Products.Where(p => p.ShopId == shop.Id)
            //        .ToListAsync();
            //    foreach (Product product in shops)
            //    {
            //        product.IsDeleted = true;
            //    }
            //    await context.SaveChangesAsync();
            //    return true;
            //}
            //return false;
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
            }
        }

        /// <summary>
        /// Вернуть магазин по названию
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Shop?> GetStoreByName(string name) 
        {
            using (var context = CreateDatabaseContext())
            {
                var ob = await context.Shops
                    .FirstOrDefaultAsync(p => p.Name == name);
                if (ob != null)
                {
                    return ob;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Получить магазин по id
        /// </summary>
        /// <param name="GetShopId">id магазина</param>
        /// <returns></returns>
        private async Task<Shop> GetShopId(int id) 
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.Set<Shop>().FindAsync(id);
            }
        }

        /// <summary>
        /// Получить id по названию магазина
        /// </summary>
        /// <param name="name">Название магазина</param>
        /// <returns>id магазина(id = -1 означает, что магазина такого нет) </returns>
        public async Task<int> GetIdByStoreName(string name)
        {
            using (var context = CreateDatabaseContext())
            {
                Shop? objectShop = await context.Shops
                    .FirstOrDefaultAsync(p => p.Name == name);
                if (objectShop == null)
                {

                    return -1;
                }
                else
                {
                    return objectShop.Id;
                }
            }

        }

        ////TODO не задача данного сервиса

        /// <summary>
        /// Проверить является ли пользователем владельцем магазина
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="nameShop"></param>
        /// <returns></returns>
        public async Task<Shop?> CheckOwnerShopUser(int id)
        {
            return null;

            //var shop = await GetShopId(id);
            //if (shop is not null)
            //{
            //    // Проверяем магазин на наличие в бд
            //    using (var context = CreateDatabaseContext())
            //    {
            //        var s = await (
            //            from o in context.ShopOwners
            //            join c in context.Shops on o.ShopId equals c.Id
            //            where   c.Id == shop.Id
            //            select c).FirstOrDefaultAsync();
            //        return s;
            //    }
            //}
            //else
            //{
            //    return null;
            //}
        }

      

        /// <summary>
        /// Вернуть список магазинов
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<Shop?>> GetShopsByIds(List<int> ids) 
        {
            using (var context = CreateDatabaseContext())
            {
                List<Shop> shops = context.Shops
                .Where(p => ids.Contains(p.Id))
                .ToList();

                return shops;
            }
        }
    }
}
