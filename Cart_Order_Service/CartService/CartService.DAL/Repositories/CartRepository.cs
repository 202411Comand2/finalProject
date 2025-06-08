using DAL.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
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
        /// Изменить количество в заказе
        /// </summary>
        public async Task<Cart> UpdateCountProductInCart(int userId, int productId, int count)
        {
            try
            {
                using (var context = CreateDatabaseContext())
                {
                    Cart cart = await context.Carts.FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);
                    cart!.Count = count;
                    await context.Carts.AddAsync(cart);
                    await context.SaveChangesAsync();
                    
                    return cart;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Удаление товара из корзины
        /// </summary>
        public async Task<bool> DeleteProductFromCart(int userId, int productId)
        {
            try
            {
                using (var context = CreateDatabaseContext())
                {
                    Cart product = await context.Carts.FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);

                    if (product is not null)
                    {
                        //await context.Carts.Remove(product);
                        await context.SaveChangesAsync();
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
