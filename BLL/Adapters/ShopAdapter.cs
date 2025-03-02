using BLL.Dto.Shop;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

