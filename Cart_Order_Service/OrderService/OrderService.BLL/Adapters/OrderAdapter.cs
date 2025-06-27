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

        /// <summary>
        /// Преобразовать из Dto в Entitie 
        /// </summary>
        public static Order ConvertFromDTOToEntity(AddOrderDto dto)
        {
            return new Order()
            {
                UserId = dto.UserId,
                ShippingMethod = dto.ShippingMethod,
                PaymentMethod = dto.PaymentMethod,
                ArriveAddress = dto.ArriveAddress,
                OrderStatus = dto.OrderStatus,
            };
        }

        /// <summary>
        /// Преобразовать из Entitie в Dto
        /// </summary>
        public static AddOrderDto ConvertFromEntitieToDTO(Order dto)
        {
            return new AddOrderDto()
            {
                UserId = dto.UserId,
                ShippingMethod = dto.ShippingMethod,
                PaymentMethod = dto.PaymentMethod,
                ArriveAddress = dto.ArriveAddress,
                OrderStatus = dto.OrderStatus,
            };
        }

        /// <summary>
        /// Преобразовать из Entitie в Dto
        /// </summary>
        public static UpdateOrderDto ConvertFromEntitieToDtoUpdate(Order dto)
        {
            return new UpdateOrderDto()
            {
                IdOrder = dto.Id,
                OrderStatus = dto.OrderStatus,
            };
        }

        /// <summary>
        /// Преобразовать из Entitie в Dto
        /// </summary>
        public static GetOrderDto ConvertFromEntitieToDtoGet(Order dto)
        {
            return new GetOrderDto()
            {
                IdOrder = dto.Id,
            };
        }
    }
}
