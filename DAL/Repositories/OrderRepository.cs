using DAL.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(IContextManager manager) : base(manager)
        {

        }

        /// <summary>
        /// Получение списка заказов
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Order>> GetUserOrder(int userId)
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.Orders.Where(p => p.UserId == userId).ToListAsync();
            }
        }
    }
}
