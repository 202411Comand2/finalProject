using BLL.Dto.Order;

namespace BLL.Orders.Abstractions
{
    public interface IOrderService
    {
        public Task<bool> AddNewOrder(AddOrderDto OrderDto);

        public Task<bool> DeleteOrder(DeleteOrderDto OrderDto);

        public Task<bool> UpdateOrder(UpdateOrderDto OrderDto);

        public Task<List<OrderDto>> GetAllOrder(GetOrderDto OrderDto);
    }
}
