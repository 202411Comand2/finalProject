using BLL.Dto.Cart;
using Domain.Entities;

namespace BLL.Adapters
{
    public class CartAdapter
    {
        /// <summary>
        /// Преобразовать из Entity в Dto
        /// </summary>
        /// <param name="cart">Корзина cart</param>
        /// <returns>CartDto</returns>
        public static CartDto ConvertFromEntitieCartToDto(Cart cart)
        {
            return new CartDto()
            {
                Id = cart.Id,
                ProductId = cart.ProductId,
                UserId = cart.UserId,
                DateCreated = cart.DateCreated,
                Count = cart.Count,
            };
        }
        /// <summary>
        /// Преобразовать из Dto в Entity 
        /// </summary>
        /// <param name="dto">Корзина CartDto</param>
        /// <returns>Cart</returns>
        public static Cart ConvertFromDtoCartToEntity(CartDto dto)
        {
            return new Cart
            {
                Id = dto.Id,
                ProductId = dto.ProductId,
                UserId = dto.UserId,
                DateCreated = dto.DateCreated,
                Count = dto.Count,
            };
        }
        public static Cart ConvertFromDtoCartToEntity(AddCartDto dto)
        {
            return new Cart
            {
                ProductId = dto.ProductId,
                UserId = dto.UserId,
                DateCreated = dto.DateCreated,
                Count = dto.Count,
            };
        }
        public static Cart ConvertFromDtoCartToEntity(UpdateCartDto dto)
        {
            return new Cart
            {
                Id = dto.Id,
                Count = dto.Count,
            };
        }
    }
}
