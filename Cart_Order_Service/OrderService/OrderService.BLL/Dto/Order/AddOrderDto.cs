using OrderService.Domain.Enums;

namespace OrderService.BLL.Dto.Order
{
    public class AddOrderDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public ShippingMethod ShippingMethod { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string ArriveAddress { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
