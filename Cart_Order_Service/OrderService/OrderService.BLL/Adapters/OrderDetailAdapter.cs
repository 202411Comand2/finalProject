using OrderService.BLL.Dto.OrderDetail;
using OrderService.Domain.Entities;

namespace OrderService.BLL.Adapters
{
    public class OrderDetailAdapter
    {
        /// <summary>
        /// Преобразовать из List<Entitie> в List<Dto></Dto>
        /// </summary>
        public static List<OrderDetailDto> ConvertFromEntitieToDTO(List<OrderDetail> items)
        {
            List<OrderDetailDto> orderDto = new();
            foreach (OrderDetail entitie in items)
            {
                orderDto.Add(new OrderDetailDto()
                {
                    Id = entitie.Id,
                    ParentId = entitie.OrderId,
                });
            }
            return orderDto;
        }


        /// <summary>
        /// Преобразовать из Entitie в Dto
        /// </summary>
        public static OrderDetailDto ConvertFromEntitieToDTO(OrderDetail entitie)
        {
            return new OrderDetailDto()
            {
                Id = entitie.Id,
                ParentId = entitie.OrderId,                
            };
        }

        /// <summary>
        /// Преобразовать из Dto в Entitie 
        /// </summary>
        public static OrderDetail ConvertFromDTOToEntity(OrderDetailDto dto)
        {
            return new OrderDetail
            {
                Id = dto.Id,
                OrderId = dto.ParentId,
            };
        }
    }
}
