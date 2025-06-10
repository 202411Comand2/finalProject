using Microsoft.EntityFrameworkCore;
using ProductService.Domain;

namespace ProductService.DAL
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
        /// Вернуть список продуктов по кластреру
        /// </summary>
        /// <param name="clusterId">Id Кластера</param>
        /// <returns></returns>
        public async Task<List<Product>> GetProductsByCluster(int clusterId) 
        {
            using (var context = CreateDatabaseContext())
            { 
                return await context.Products.Where(p => p.ClusterId == clusterId).ToListAsync();
            }
        }
        /// <summary>
        /// Вернуть продукты по кластерам
        /// </summary>
        /// <param name="idsCluster"></param>
        /// <returns></returns>
        public async Task<List<Product>> GetProductsByClusters(List<int> idsCluster) 
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.Products
                .Where(e => idsCluster.Contains(e.Id)) 
                .ToListAsync();
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
                    .Include(cl => cl.ClusterID)
                    .Include(sh => sh.ShopId)
                    .Where(p => p.Id == idProduct).FirstOrDefaultAsync();
            }
        }

    }
}
