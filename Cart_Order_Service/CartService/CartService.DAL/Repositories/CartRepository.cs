using CartService.DAL.Abstractions;
using CartService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CartService.DAL.Repositories
{
    public class CartRepository : BaseRepository<Cart>
    {
        public CartRepository(IContextManager manager) : base(manager)
        {

        }

        /// <summary>
        /// Добавление товара в корзину
        /// </summary>
        public override Task<Cart> Add(Cart cart)
        {
            try
            {
                return base.Add(cart);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Получение списка товаров в корзине
        /// </summary>
        public async Task<List<Cart>> GetListProducts(int userId)
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.Carts.Where(p => p.UserId == userId).ToListAsync();
            }
        }

        /// <summary>
        /// Получение товара из корзины
        /// </summary>
        public async Task<Cart> GetProductInCart(int idCart)
        {
            try
            {
                using (var context = CreateDatabaseContext())
                {
                    Cart product = await context.Carts.FirstAsync(x => x.UserId == idCart);

                    if (product is not null)
                    {
                        await context.SaveChangesAsync();
                        return product;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
