using OrderService.Domain.Enums;

namespace OrderService.BLL.Dto.OrderDetail
{
    public class UpdateOrderDetailDto
    {
        public int IdOrderDetail { get; set; }

        public StatusPosition StatusPosition { get; set; }
    }
}
