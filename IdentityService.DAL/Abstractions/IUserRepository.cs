using IdentityService.Domain;
using IdentityService.Domain.Abstractions;

namespace IdentityService.DAL.Abstractions
{
	public interface IUserRepository<T> : IRepository<T> where T : class, IEntity
	{
        public Task<T?> GetByEmail(string email, CancellationToken? cancellationToken = null);
        public Task<T?> GetByPhone(string phone, CancellationToken? cancellationToken = null);
        public Task<bool> ChangePassword(int userId, string newPassword, CancellationToken? cancellationToken = null);
    }
}
