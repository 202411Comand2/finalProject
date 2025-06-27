using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OwnerService.Domain.Entities;
using ManagersShopsService.BLL.Dto;
using AuthService.BLL.Dto;

namespace ManagersShopsService.BLL
{
    public class ManagersShopsAdapter
    {
        /// <summary>
        /// Преобразовать из Entitie в Dto
        /// </summary>
        /// <param name="shop">Магазин Entitie</param>
        /// <returns>CommentDto</returns>
        public static AddManagersShopsDto ConvertFromEntitieToDTO(ManagersShops model)
        {
            return new AddManagersShopsDto()
            {
              UserId = model.UserId,
              RoleUser = model.RoleUser,
              NameShop = model.NameShop,
              ShopId = model.ShopId,
                UserName = model.UserName
            };
        }

        /// <summary>
        /// Преобразовать из Dto в Entitie 
        /// </summary>
        /// <param name="shopDto">Магазин Entitie</param>
        /// <returns>Comment</returns>
        public static ManagersShops ConvertFromDTOToEntity(AddManagersShopsDto model)
        {
            return new ManagersShops()
            {
                UserId = model.UserId,
                RoleUser = model.RoleUser,
                NameShop = model.NameShop,
                ShopId = model.ShopId,
                UserName=model.UserName
            };
        }


        public static ManagersShops ConvertFromDTOToEntity(DeleteManagersShopsDto model)
        {
            return new ManagersShops()
            {
                UserId = model.UserId,
                ShopId = model.ShopId,
                Id=model.UserId
            };
        }


        public static ManagersShops ConvertFromDTOToEntity(GetManagersShopsDto model)
        {
            return new ManagersShops()
            {
                UserId = model.UserId,
                ShopId = model.ShopId,
                NameShop = model.NameShop,
                UserName = model.UserName,
                RoleUser = model.RoleUser,
                Id = model.Id

            };
        }

        public static List<GetManagersShopsDto > ConvertFromEntitieToDTO(List<ManagersShops> Items)
        {
            List<GetManagersShopsDto> list = new();
            foreach (var item in Items)
            {
                list.Add(new GetManagersShopsDto()
                {
                    UserId = item.UserId,
                    ShopId = item.ShopId,
                    NameShop = item.NameShop,
                    UserName = item.UserName,
                    RoleUser = item.RoleUser,
                    Id=item.Id

                });
            }
            return list;
        }


    }
}

