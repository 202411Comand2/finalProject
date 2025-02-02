using DAL.Abstractions;
using Domain.Entities;

namespace DAL.Repositories
{
    public class ShopOwnerRepository : BaseRepository<ShopOwner>
    {
        private readonly IContextManager _contextManager;

        public ShopOwnerRepository(IContextManager manager) : base(manager)
        {
            _contextManager = manager;
        }

        /// <summary>
        /// Зарегистрировать владельца магазина
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override Task<ShopOwner> Add(ShopOwner entity)
        {
            return base.Add(entity);
        }

    }
}
