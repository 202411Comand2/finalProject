using BLL.Dto;
using Domain.Entities;

namespace BLL.Adapters
{
    public class OrderAdapter
    {
        /// <summary>
        /// Преобразовать коллекцию из Entitie в OrderDto 
        /// </summary>
        /// <param name="Orders">коллекция избранных позиций Entitie</param>
        /// <returns>Comment</returns>
        public static OrderDto ConvertFromEntityToOrderDto(Order Order) 
        {
            return new OrderDto
            {
                Id = Order.Id,
                //ProductId = Order.IdProduct,
                UserId = Order.UserId
            };
      }

        /// <summary>
        /// Преобразовать коллекцию из Entitie в OrderDto 
        /// </summary>
        /// <param name="Orders">коллекция избранных позиций Entitie</param>
        /// <returns>Comment</returns>
        public static List<OrderDto> ConvertFromEntityToOrderDto(List<Order> Orders)
        {
            List<OrderDto> shopDtos = new List<OrderDto>();
            foreach (Order Order in Orders)
            {
                shopDtos.Add(new OrderDto
                {
                    Id = Order.Id,
                    //ProductId = Order.IdProduct,
                    UserId = Order.UserId,
                });
            }
            return shopDtos;
        }
    }
}
