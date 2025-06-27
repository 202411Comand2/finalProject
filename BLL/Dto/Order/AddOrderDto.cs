using FinalProjectEntityDataBase.Enums;

namespace BLL.Dto.Order
{
    public class AddOrderDto
    {
        public int ProductId { get; set; }
        public int ShopId { get; set; }
        public DateTime DateCreated { get; set; }
        public int Count { get; set; }
        public DateTime ArriveDate { get; set; }
        public OrderState State { get; set; }
    }
}
