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

   

        ////TODO спросить у ребят, где мы ищем пользователя в userRepository или в этом репозитории?
        /// <summary>
        /// Найти пользователя
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <returns></returns>
        public Task<bool> SearchUser(int id) 
        {
            return null;
        }
    }
}
