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
        public static UpdateCartDto ConvertFromEntityToCartDto(Cart cart)
        {
            return new UpdateCartDto
            {
                Id = cart.Id,
                Count = cart.Count,
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
            foreach (var Cart in carts)
            {
                shopDtos.Add(new CartDto
                {
                    Id = Cart.Id,
                    ProductId = Cart.ProductId,
                    UserId = Cart.UserId,
                    Count = Cart.Count,
                });
            }
            return shopDtos;
        }

        /// <summary>
        /// Преобразовать из Dto в Entitie 
        /// </summary>
        public static Cart ConvertFromDTOToEntity(AddCartDto dto)
        {
            return new Cart()
            {
                UserId = dto.UserId,
                Count = dto.Count,
                ProductId = dto.ProductId,
                Price = dto.Price,
                Discount = dto.Discount,
                DateCreated =dto.DateCreated,
            };
        }

        /// <summary>
        /// Преобразовать из Entitie в Dto
        /// </summary>
        public static CartDto ConvertFromEntitieToDTO(Cart dto)
        {
            return new CartDto()
            {
                UserId = dto.UserId,
                Count = dto.Count,
                ProductId = dto.ProductId,
                /*Price = dto.Price,
                Discount = dto.Discount,
                DateCreated = dto.DateCreated,*/
            };
        }
    }
}
