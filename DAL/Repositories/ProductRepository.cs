using DAL.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(IContextManager manager) : base(manager)
        {

        }

        /// <summary>
        /// Получить продукты по магазину
        /// </summary>
        /// <param name="shopId">Id магазина</param>
        /// <returns></returns>
        public async Task<List<Product>> GetShopProducts(int shopId) 
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.Products.Where(p => p.ShopId == shopId).ToListAsync();
            }
        }

       
        /// <summary>
        /// Получить продукт со связими (one to one)
        /// </summary>
        /// <param name="idProduct">id продукта</param>
        /// <returns></returns>
        public async Task<Product> GetWithInclude(int idProduct)
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.Products
                    .Include(cl => cl.Cluster)
                    .Include(sh => sh.Shop)
                    .Where(p => p.Id == idProduct).FirstOrDefaultAsync();
            }
        }

    }
}
