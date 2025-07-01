using Microsoft.EntityFrameworkCore;
using OrderService.DAL.Abstractions;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;

namespace OrderService.DAL.Repositories
{
    public class OrderDetailRepository : BaseRepository<OrderDetail>
    {

        public OrderDetailRepository(IContextManager manager) : base(manager)
        {

        }

        /// <summary>
        /// Добавить детализацию заказа
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
        /// Удалить детализацию заказа
        /// </summary>
        public async Task<bool> DeleteOrderDetail(int orderId)
        {
            try
            {
                using (var context = CreateDatabaseContext())
                {
                    //var orderDetails = await context.OrderDetails.Where(x => x.Id == orderId).ToListAsync();

                    //await context.OrderDetails.Remove(orderDetails);
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
        /// Получить всю детализацию по заказу
        /// </summary>
        public async Task<List<OrderDetail>> GetListOrders(int orderId)
        {
            using (var context = CreateDatabaseContext())
            {
                //return await context.OrderDetails.Where(p => p.OrderId == orderId).ToListAsync();
                return null;
            }
        }

        /// <summary>
        /// Изменить статус позиции в заказе
        /// </summary>
        public async Task<OrderDetail> UpdateStatusAsync(int id, int statusPosition)
        {
            try
            {
                using (var context = CreateDatabaseContext())
                {
                    /*OrderDetail orderDetail = await context.OrderDetails.FirstAsync(x => x.Id == id);
                    orderDetail!.StatusPosition = statusPosition;
                    await context.OrderDetails.AddAsync(orderDetail);
                    await context.SaveChangesAsync();*/

                    return null; // orderDetail;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}