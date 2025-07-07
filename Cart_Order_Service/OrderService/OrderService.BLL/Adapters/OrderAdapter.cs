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
            foreach (var entitie in items)
            {
                orderDto.Add(new OrderDto()
                {
                    Id = entitie.Id,
                    UserId = entitie.UserId,
                    DateCreated = entitie.DateCreated,
                    ArriveDate = entitie.ArriveDate,
                    ShippingMethod = entitie.ShippingMethod,
                    PaymentMethod = entitie.PaymentMethod,
                    ArriveAddress = entitie.ArriveAddress,
                    OrderStatus = entitie.OrderStatus,
                    DateOrderStatus = entitie.DateOrderStatus,
                });
            }
            return orderDto;
        }

        /// <summary>
        /// Преобразовать из Dto в Entitie 
        /// </summary>
        public static OrderDto ConvertFromDTOToEntity(OrderDto dto)
        {
            return new OrderDto
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
        public static OrderDto ConvertFromEntitieToDTO(OrderDto dto)
        {
            return new OrderDto()
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
        public static OrderDto ConvertFromEntitieToDtoUpdate(OrderDto dto)
        {
            return new OrderDto()
            {
                Id = dto.Id,
                OrderStatus = dto.OrderStatus,
            };
        }

        /// <summary>
        /// Преобразовать из Entitie в Dto
        /// </summary>
        public static OrderDto ConvertFromEntitieToDtoGet(Order dto)
        {
            return new OrderDto()
            {
                Id = dto.Id,
                UserId = dto.UserId,
                DateCreated = dto.DateCreated,
                ArriveDate = dto.ArriveDate,
                ShippingMethod = dto.ShippingMethod,
                PaymentMethod = dto.PaymentMethod,
                ArriveAddress = dto.ArriveAddress,
                OrderStatus = dto.OrderStatus,
                DateOrderStatus = dto.DateOrderStatus,
            };
        }
    }
}