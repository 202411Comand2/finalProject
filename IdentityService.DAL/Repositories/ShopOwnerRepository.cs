using IdentityService.DAL.Abstractions;
using IdentityService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityService.DAL.Repositories
{
    public class ShopOwnerRepository : IShopOwnerRepository<ShopOwner>
    {
        public IContextManager ContextManager { get; private set; }
        private readonly ILogger _logger;
        public ShopOwnerRepository(IContextManager contextManager, ILogger<ShopOwnerRepository> logger)
        {
            ContextManager = contextManager;
            _logger = logger;
            _logger.LogDebug("New instance of ShopOwnerRepository was initialized");
        }
        public async Task<bool> Delete(int id, CancellationToken? cancellationToken = null)
        {
            using(var context = ContextManager.CreateDatabaseContext())
            {
                var rowsAffected = await context.ShopOwners
                    .Where(x => x.Id == id)
                    .ExecuteDeleteAsync();

                return rowsAffected > 0;
            }
        }
        public async Task<ShopOwner?> Get(int id, CancellationToken? cancellationToken = null)
        {
            using(var context = ContextManager.CreateDatabaseContext())
            {
                return await context.ShopOwners.FirstOrDefaultAsync(x => x.Id == id);
            }
        }
        public async Task<IList<ShopOwner>> GetAll(CancellationToken? cancellationToken = null)
        {
            _logger.LogCritical("Unsafe GetAll method called!!!");
            using(var context = ContextManager.CreateDatabaseContext())
            {
                return await context.ShopOwners.ToListAsync();
            }
        }
        public async Task<bool> Update(ShopOwner entity, CancellationToken? cancellationToken = null)
        {
            using (var context = ContextManager.CreateDatabaseContext())
            {
                try
                {
                    var attachedEntity = context.ShopOwners.FindAsync(entity.Id);
                    context.Entry(attachedEntity).CurrentValues.SetValues(entity);
                    await context.SaveChangesAsync();
                    _logger.LogDebug($"ShopOwner id:{entity.Id} updated successfully");
                    
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message, ex.StackTrace);
                    return false;
                }
            }
            return true;
        }
        public async Task<ShopOwner> Add(ShopOwner entity, CancellationToken? cancellationToken = null)
        {
            using (var context = ContextManager.CreateDatabaseContext())
            {
                if(entity.IsHost == true)
                {
                    var hostsList = await context.ShopOwners.Where(x => x.ShopId == entity.ShopId
                    && x.IsDeleted == false && x.IsHost == true)
                    .ToListAsync();
                    if (hostsList.Count > 0) throw new Exceptions.DataAccessException("Shop host already exists");
                    _logger.LogError("Unable to add new ShopOwner(host)");
                }
                    
                await context.ShopOwners.AddAsync(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
        public async Task<ShopOwner?> Get(int userId, int shopId, CancellationToken? cancellationToken = null)
        {
            using (var context = ContextManager.CreateDatabaseContext())
            {
                return await context.ShopOwners.Where(x => (x.UserId == userId && x.ShopId == shopId)
                    && x.IsDeleted == false)
                    .FirstOrDefaultAsync();
            }
        }
        public async Task<List<ShopOwner>> GetAllByUserId(int userId, CancellationToken? cancellationToken = null)
        {
            using (var context = ContextManager.CreateDatabaseContext())
            {
                return await context.ShopOwners.Where(x => x.IsDeleted == false)
                    .Where(x => x.UserId == userId)
                    .ToListAsync();
            }
        }
        public async Task<List<ShopOwner>> GetAllByShopId(int shopId, CancellationToken? cancellationToken = null)
        {
            using (var context = ContextManager.CreateDatabaseContext())
            {
                return await context.ShopOwners.Where(x => x.IsDeleted == false && x.ShopId == shopId).ToListAsync();
            }
        }
    }
}
