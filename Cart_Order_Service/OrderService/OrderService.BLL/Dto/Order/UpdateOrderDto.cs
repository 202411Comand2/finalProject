using OrderService.Domain.Enums;

namespace OrderService.BLL.Dto.Order
{
    public class UpdateOrderDto
    {
        public int IdOrder { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}
