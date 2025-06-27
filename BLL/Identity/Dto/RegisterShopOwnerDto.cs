using BLL.Identity.Abstractions;
using Domain.Entities;

namespace BLL.Identity.Dto
{
    public class RegisterShopOwnerDto : IDto<ShopOwner, RegisterShopOwnerDto>
    {
        public int ShopId { get; set; }
        public int UserId { get; set; }
        public bool IsHost { get; set; }

        public RegisterShopOwnerDto Parse(ShopOwner entity)
        {
            ShopId = entity.ShopId;
            UserId = entity.UserId;
            IsHost = entity.IsHost;
            return this;
        }

        public ShopOwner ToEntity()
        {
            return new ShopOwner 
            { 
                ShopId = ShopId, 
                UserId = UserId,
                IsHost = IsHost
            };
        }
    }
}
