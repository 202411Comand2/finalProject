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
        /// Вернуть рейтинг товара
        /// </summary>
        /// <param name="idProduct"></param>
        /// <returns></returns>
        public async Task GetIncludeReting(int idProduct) 
        {
        
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
                    .Include(c => c.RatingId)
                    .Include(cl => cl.Cluster)
                    .Include(sh => sh.Shop)
                    .Where(p => p.Id == idProduct).FirstOrDefaultAsync();
            }
        }

    }
}
