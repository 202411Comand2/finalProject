using IdentityService.Domain;
using IdentityService.Domain.Abstractions;

namespace IdentityService.DAL.Abstractions
{
    public interface IShopOwnerRepository<T> : IRepository<T> where T : class, IEntity
    {
        public Task<List<ShopOwner>> GetAllByUserId(int userId, CancellationToken? cancellationToken = null);
        public Task<List<ShopOwner>> GetAllByShopId(int shopId, CancellationToken? cancellationToken = null);
    }
}
