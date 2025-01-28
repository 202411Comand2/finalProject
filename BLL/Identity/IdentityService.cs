using BLL.Abstractions;
using DAL.Abstractions;
using DAL.Repositories;
using Domain.Entities;

namespace BLL.Identity
{
    public class IdentityService
    {
        private readonly UserRepository _userRepository;
        private readonly AuthTokenRepository _authTokenRepository;
        private readonly IGenericDataHasher<string> _passwordHasher;

        public IdentityService(IContextManager contextManager)
        {
            _userRepository = new UserRepository(contextManager);
            _authTokenRepository = new AuthTokenRepository(contextManager);
            _passwordHasher = new StringHasher();
        }

        public async Task CreateUser(User user, CancellationToken ct)
        {
			throw new NotImplementedException("Пока не сделал");
		}
        public async Task UpdateUser(User user)
        {
			throw new NotImplementedException("Пока не сделал");
		}
        public async Task ChangePassword(User user)
        {
			throw new NotImplementedException("Пока не сделал");
		}
        public async Task<User> AuthUserByEmail(string email, string password)
        {
            throw new NotImplementedException("Пока не сделал");
        }
        public async Task<User> AuthUserByPhone(string phone, string password)
        {
			throw new NotImplementedException("Пока не сделал");
		}
    }
}
