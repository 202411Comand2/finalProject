using BLL.Dto.Order;
using BLL.Orders.Abstractions;
using DAL.Abstractions;
using DAL.Repositories;
using Domain.Entities;

namespace BLL.Orders
{
    /// <summary>
    /// Сервис по работе с заказами
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly OrderRepository _orderRepository;
        public OrderService(IContextManager contextManager) => _orderRepository = new OrderRepository(contextManager);
        public async Task<bool> AddNewOrder(AddOrderDto orderDto)
        {
            Order orderEntity = Adapters.OrderAdapter.ConvertFromDtoOrderToEntity(orderDto);
            if ((await _orderRepository.Add(orderEntity)) is not null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> DeleteOrder(DeleteOrderDto orderDto)
        {
            Order orderEntity = await _orderRepository.Get(orderDto.Id);
            if (orderEntity == null)
            {
                return false;

            }
            await _orderRepository.Update(orderEntity);
            return true;
        }
        public async Task<bool> UpdateOrder(UpdateOrderDto orderDto)
        {
            Order orderEntity = Adapters.OrderAdapter.ConvertFromDtoOrderToEntity(orderDto);
            Order orderNew = await _orderRepository.Get(orderEntity.Id);
            if (orderNew is null)
            {
                return false;
            }
            orderEntity.Count = orderNew.Count;
            await _orderRepository.Update(orderEntity);
            return true;
        }
        public async Task<List<OrderDto>> GetAllOrder(GetOrderDto orderDto)
        {
            List<OrderDto> orders = new List<OrderDto>();
            foreach (Order order in await _orderRepository.GetUserOrder(orderDto.UserId))
            {
                orders.Add(Adapters.OrderAdapter.ConvertFromEntityOrderToDto(order));
            }
            return orders;
        }
    }
}
