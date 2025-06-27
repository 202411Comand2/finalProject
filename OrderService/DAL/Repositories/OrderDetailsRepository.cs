using DAL.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{    
    public class OrderDetailsRepository : BaseRepository<OrderDetail>
    {
        public OrderDetailsRepository(IContextManager manager) : base(manager)
        {
        }

        /// <summary>
        /// Добавить заказ
        /// </summary>
        public override Task<OrderDetail> Add(OrderDetail orderDetail)
        {
            try
            {
                return base.Add(orderDetail);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Удалить детализацию по заказу
        /// </summary>
        public async Task<bool> DeleteOrderDetail(int orderId)
        {
            try
            {
                using (var context = CreateDatabaseContext())
                {
                    var orderDetail = await context.OrderDetails.Where(x => x.OrderId == orderId).ToListAsync();

                    //await context.Orders.RemoveRange(orderDetail);
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
        /// Получить детализацию по заказам
        /// </summary>
        public async Task<List<OrderDetail>> GetListOrdersDetail(int orderId)
        {
            using (var context = CreateDatabaseContext())
            {
                return await context.OrderDetails.Where(p => p.OrderId == orderId).ToListAsync();
            }
        }    
    }
}
