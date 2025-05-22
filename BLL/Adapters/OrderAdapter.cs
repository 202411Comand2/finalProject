using BLL.Dto.Cart;
using BLL.Dto.Order;
using Domain.Entities;
using FinalProjectEntityDataBase.Enums;
using Order = Domain.Entities.Order;

namespace BLL.Adapters
{
    public class OrderAdapter
    {
        /// <summary>
        /// Преобразовать из Entity в Dto
        /// </summary>
        /// <param name="cart">Корзина cart</param>
        /// <returns>OrderDto</returns>
        public static OrderDto ConvertFromEntityOrderToDto(Order order)
        {
            return new OrderDto()
            {
                Id = order.Id,
                State = order.State,
                UserId = order.UserId,
                DateCreated = order.DateCreated,
                ArriveDate = order.ArriveDate,
            };
        }

        /// <summary>
        /// Преобразовать из Dto в Entity 
        /// </summary>
        /// <param name="OrderDto">Корзина Cart</param>
        /// <returns>Order</returns>
        public static Order ConvertFromDtoOrderToEntity(OrderDto dto)
        {
            return new Order
            {
                Id = dto.Id,
                State = dto.State,
                UserId = dto.UserId,
                DateCreated = dto.DateCreated,
                ArriveDate = dto.ArriveDate,
            };
        }

        public static Order ConvertFromDtoOrderToEntity(AddOrderDto dto)
        {
            return new Order
            {
                ProductId = dto.ProductId,
                ShopId = dto.ShopId,
                DateCreated = dto.DateCreated,
                Count = dto.Count,
                ArriveDate = dto.ArriveDate,
                State = dto.State,
            };
        }

        public static Order ConvertFromDtoOrderToEntity(UpdateOrderDto dto)
        {
            return new Order
            {
                Id = dto.Id,
                Count = dto.Count,
                ArriveDate = dto.ArriveDate,
                State = dto.State,
            };
        }
    }
}
