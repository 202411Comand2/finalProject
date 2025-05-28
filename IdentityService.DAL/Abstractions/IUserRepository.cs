using IdentityService.Domain;
using IdentityService.Domain.Abstractions;

namespace IdentityService.DAL.Abstractions
{
	public interface IUserRepository<T> : IRepository<T> where T : class, IEntity
	{
        public Task<T?> GetByEmail(string email);
        public Task<T?> GetByPhone(string phone);
        public Task<bool> ChangePassword(int userId, string newPassword);
    }
}
