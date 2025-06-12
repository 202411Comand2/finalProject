using Microsoft.EntityFrameworkCore;
using OrderService.DAL.Abstractions;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;

namespace OrderService.DAL.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {

        public OrderRepository(IContextManager manager) : base(manager)
        {

        }

        /// <summary>
        /// Добавить заказ
        /// </summary>
        public override Task<Order> Add(Order order)
        {
            try
            {
                return base.Add(order);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Отменить заказ
        /// </summary>
        public async Task<bool> DeleteOrder(int orderId)
        {
            try
            {
                using (var context = CreateDatabaseContext())
                {
                    var order = await context.Orders.FirstAsync(x => x.Id == orderId);

                    //await context.Orders.Remove(order);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Получить все заказы
        /// </summary>
        public async Task<List<Order>> GetListOrders(int userId)
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.Orders.Where(p => p.UserId == userId).ToListAsync();
            }
        }


        /// <summary>
        /// Получить текущий заказ
        /// </summary>
        public async Task<Order> GetCurrentOrder(int id)
        {
            try
            {
                using (var context = CreateDatabaseContext())
                {
                    return await context.Orders.FirstAsync(p => p.Id == id);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Изменить статус заказа
        /// </summary>
        public async Task<Order> UpdateStatusAsync(int id, OrderStatus orderStatus)
        {
            try
            {
                using (var context = CreateDatabaseContext())
                {
                    Order order = await context.Orders.FirstAsync(x => x.Id == id);
                    order!.OrderStatus = orderStatus;
                    await context.Orders.AddAsync(order);
                    await context.SaveChangesAsync();

                    return order;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}