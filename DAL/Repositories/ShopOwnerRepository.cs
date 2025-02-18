using DAL.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
        public override async Task<ShopOwner> Add(ShopOwner entity)
        {
            if (entity.IsHost == true)
            {
				using (var context = CreateDatabaseContext())
				{
					var hostsList = await context.ShopOwners.Where(x => x.ShopId == entity.ShopId
						&& x.IsDeleted == false && x.IsHost == true)
                        .ToListAsync();
                    bool hostExists = hostsList.Any();
                    if (hostExists == false)
                    {
                        await context.ShopOwners.AddAsync(entity);
                        await context.SaveChangesAsync();
                        return entity;
                    }
                    return null;
				}
			}
            else return await base.Add(entity);
        }

        public async Task<ShopOwner?> Get(int userId, int shopId)
        {
            using(var context = CreateDatabaseContext())
            {
                return await context.ShopOwners.Where(x => (x.UserId == userId && x.ShopId == shopId)
                    && x.IsDeleted == false)
                    .FirstOrDefaultAsync();
            }
        }
        public async Task<List<ShopOwner>> GetAllByUserId(int userId)
        {
            using(var context = CreateDatabaseContext())
            {
                return await context.ShopOwners.Where(x => x.IsDeleted == false)
                    .Where(x => x.UserId == userId)
                    .ToListAsync();
            }
        }
    }
}
