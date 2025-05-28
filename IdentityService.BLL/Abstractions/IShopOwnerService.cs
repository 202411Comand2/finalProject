using BLL.Identity.Dto;
using IdentityService.Domain;

namespace IdentityService.BLL.Abstractions
{
    public interface IShopOwnerService
    {
        public Task<bool> Add(RegisterShopOwnerDto dto, CancellationToken? cancellationToken = null);
        public Task Update();
        public Task Delete();
        public Task<List<ShopOwner>> GetAllByUser(int userId, CancellationToken? cancellationToken = null);
        public Task GetAllByShop(int shopId, CancellationToken? cancellationToken = null);
    }
}
