using BLL.Identity.Dto;
using IdentityService.BLL.Abstractions;
using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Exceptions;
using IdentityService.Domain;
using Microsoft.Extensions.Logging;

namespace IdentityService.BLL
{
    public class ShopOwnerService : IShopOwnerService
    {
        private readonly IShopOwnerRepository<ShopOwner> _repository;
        private readonly ILogger _logger;
        public ShopOwnerService(IShopOwnerRepository<ShopOwner> repository,
            ILogger<ShopOwnerService> logger)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogDebug("New instance of ShopOwnerService class was created");
        }

        public async Task<bool> Add(RegisterShopOwnerDto dto, CancellationToken? cancellationToken = null)
        {
            try
            {
                var result = await _repository.Add(dto.ToEntity());
                if (result != null) { return true; }
                else { return false; }
            }
            catch (DataAccessException ex)
            {
                _logger.LogInformation(ex, ex.Message, ex.StackTrace);
                return false;
            }
        }
        public Task Update()
        {
            throw new NotImplementedException();
        }
        public Task Delete()
        {
            throw new NotImplementedException();
        }
        public async Task<List<ShopOwner>> GetAllByUser(int userId, CancellationToken? cancellationToken = null)
        {
            return await _repository.GetAllByUserId(userId, cancellationToken);
        }
        public async Task GetAllByShop(int shopId, CancellationToken? cancellationToken = null)
        {
            return await _repository.GetAllByShopId(shopId, cancellationToken);
        }
    }
}
