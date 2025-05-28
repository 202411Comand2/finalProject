using IdentityService.DAL.Abstractions;
using IdentityService.Domain;

namespace IdentityService.BLL.Abstractions
{
    public interface IShopService : IRepository<ShopOwner>
    {
    }
}
