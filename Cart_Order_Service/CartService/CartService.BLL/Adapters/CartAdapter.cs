using CartService.BLL.Dto;
using CartService.Domain.Entities;

namespace CartService.BLL.Adapters
{
    public class CartAdapter
    {
        /// <summary>
        /// Преобразовать коллекцию из Entitie в CartDto 
        /// </summary>
        /// <param name="Carts">коллекция избранных позиций Entitie</param>
        /// <returns>Comment</returns>
        public static CartDto ConvertFromEntityToCartDto(Cart cart) 
        {
            return new CartDto
            {
                Id = cart.Id,
                ProductId = cart.ProductId,
                UserId = cart.UserId
            };
      }

        /// <summary>
        /// Преобразовать коллекцию из Entitie в CartDto 
        /// </summary>
        /// <param name="Carts">коллекция избранных позиций Entitie</param>
        /// <returns>Comment</returns>
        public static List<CartDto> ConvertFromEntityToCartDto(List<Cart> carts)
        {
            List<CartDto> shopDtos = new List<CartDto>();
            foreach (Cart Cart in carts)
            {
                shopDtos.Add(new CartDto
                {
                    Id = Cart.Id,
                    ProductId = Cart.ProductId,
                    UserId = Cart.UserId,
                });
            }
            return shopDtos;
        }
    }
}
