using BLL.Dto.Favorite;
using BLL.Dto.Shop;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Adapters
{
    public class FavoriteAdapter
    {
        /// <summary>
        /// Преобразовать коллекцию из Entitie в FavoriteDto 
        /// </summary>
        /// <param name="favorites">коллекция избранных позиций Entitie</param>
        /// <returns>Comment</returns>
        public static List<FavoriteDto> ConvertFromEntityToFavoriteDto(List<Favorite> favorites)
        {
            List<FavoriteDto> shopDtos = new List<FavoriteDto>();
            foreach (Favorite favorite in favorites)
            {
                shopDtos.Add(new FavoriteDto
                {
                    Id = favorite.Id,
                    ProductId = favorite.IdProduct,
                    UserId = favorite.UserId,
                });
            }
            return shopDtos;
        }
    }
}
