using BLL.Dto.Shop;
using Domain.Entities;

namespace BLL.Adapters
{
    public class ShopAdapter
    {
        /// <summary>
        /// Преобразовать из Entitie в Dto
        /// </summary>
        /// <param name="shop">Магазин Entitie</param>
        /// <returns>CommentDto</returns>
        public static ShopDto ConvertFromEntitieToDTO(Shop shop)
        {
            return new ShopDto()
            {
                Id = shop.Id,
                Name = shop.Name,
                IsDelete = shop.IsDelete,
            };
        }
        /// <summary>
        /// Преобразовать из Dto в Entitie 
        /// </summary>
        /// <param name="shopDto">Магазин Entitie</param>
        /// <returns>Comment</returns>
        public static Shop ConvertFromDTOToEntity(ShopDto shopDto)
        {
            return new Shop
            {
                Id = shopDto.Id,
                Name = shopDto.Name,
                IsDelete = shopDto.IsDelete,
            };
        }
        /// <summary>
        /// Преобразовать из Entitie в ShopDto 
        /// </summary>
        /// <param name="shopDto">Магазин Entitie</param>
        /// <returns>Comment</returns>
        public static ShopDto ConvertFromToEntityShopDto(Shop shop) 
        {
            return new ShopDto
            {
                Id = shop.Id,
                Name = shop.Name,
                IsDelete = shop.IsDelete,
            };
        }
        /// <summary>
        /// Преобразовать коллекцию из Entitie в ShopDto 
        /// </summary>
        /// <param name="shopDto">коллекция магазин Entitie</param>
        /// <returns>Comment</returns>
        public static List<ShopDto> ConvertFromToEntityShopDto(List<Shop> shops) 
        {
            List<ShopDto> shopDtos = new List<ShopDto>();
            foreach (Shop shop in shops) 
            {
                shopDtos.Add(new ShopDto
                {
                    Id = shop.Id,
                    Name = shop.Name,
                    IsDelete = shop.IsDelete,
                });
            }
            return shopDtos;
        }
    }
}

