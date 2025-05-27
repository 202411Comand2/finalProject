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
        /// Получение списка товаров в корзине
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Cart>> GetUserCart(int userId)
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.Carts.Where(p => p.UserId == userId).ToListAsync();
            }
        }

        /// <summary>
        /// Добавление товара в корзину
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<bool> AddProductToCart(Cart cart)
        {
            using (var context = CreateDatabaseContext())
            {
                var cartList = await context.Carts.Where(x => x.UserId == cart.UserId && x.ProductId == cart.ProductId).ToListAsync();
                
                bool cartExists = cartList.Any();
                if (cartExists == false)
                {
                    await context.Carts.AddAsync(cart);
                    await context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    UpdateCountProductInCart(cart.UserId, cart.ProductId, cart.Count);
                    return true;
                }  
            }
        }

        /// <summary>
        /// Изменить количество в заказе
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<bool> UpdateCountProductInCart(int userId, int productId, decimal count)
        {
            try
            {
                using (var context = CreateDatabaseContext())
                {
                    Cart cart = await context.Carts.FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);
                    cart!.Count = count;
                    await context.Carts.AddAsync(cart);
                    await context.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Удаление товара из корзины
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteProductFromCart(Cart cart)
        {
            using (var context = CreateDatabaseContext())
            {
                var cartList = await context.Carts.Where(x => x.UserId == cart.UserId && x.ProductId == cart.ProductId).ToListAsync();

                bool cartExists = cartList.Any();
                if (cartExists == true)
                {
                    //object value = await context.Carts.RemoveAsync(cart);
                    await context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
        }
    }
}
