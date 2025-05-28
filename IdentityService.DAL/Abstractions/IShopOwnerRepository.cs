using IdentityService.Domain.Abstractions;

namespace IdentityService.DAL.Abstractions
{
    public interface IShopOwnerRepository<T> : IRepository<T> where T : class, IEntity
    {
    }
}
