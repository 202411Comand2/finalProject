using FinalProjectEntityDataBase.Enums;

namespace BLL.Dto.Order
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public DateTime ArriveDate { get; set; }
        public OrderState State { get; set; }
    }
}
