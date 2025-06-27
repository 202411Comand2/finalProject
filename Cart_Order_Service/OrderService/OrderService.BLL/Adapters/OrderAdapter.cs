using OrderService.BLL.Dto.Order;
using OrderService.Domain.Entities;

namespace OrderService.BLL.Adapters
{
    public class OrderAdapter
    {
        /// <summary>
        /// Преобразовать из List<Entitie> в List<Dto></Dto>
        /// </summary>
        public static List<OrderDto> ConvertFromEntitieToDTO(List<Order> items)
        {
            List<OrderDto> orderDto = new();
            foreach (Order entitie in items)
            {
                orderDto.Add(new OrderDto()
                {
                    Id = entitie.Id,
                    UserId = entitie.UserId,
                });
            }
            return orderDto;
        }


        /// <summary>
        /// Преобразовать из Entitie в Dto
        /// </summary>
        public static OrderDto ConvertFromEntitieToDTO(Order entitie)
        {
            return new OrderDto()
            {
                Id = entitie.Id,
                UserId = entitie.UserId,                
            };
        }

        /// <summary>
        /// Преобразовать из Dto в Entitie 
        /// </summary>
        public static Order ConvertFromDTOToEntity(OrderDto dto)
        {
            return new Order
            {
                Id = dto.Id,
                UserId = dto.UserId,
            };
        }
    }
}
