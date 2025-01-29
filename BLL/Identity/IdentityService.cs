using BLL.Abstractions;
using DAL.Abstractions;
using DAL.Repositories;
using Domain.Entities;
using Domain.Enums;

namespace BLL.Identity
{
    public class IdentityService
    {
        private readonly UserRepository _userRepository;
        private readonly AccessTokenRepository _accessTokenRepository;
        private readonly IGenericDataHasher<string> _passwordHasher;

        public IdentityService(IContextManager contextManager)
        {
            _userRepository = new UserRepository(contextManager);
            _accessTokenRepository = new AccessTokenRepository(contextManager);
            _passwordHasher = new StringHasher();
        }

        public async Task<AccessToken> GetGuestToken(string deviceName, string deviceIp)
        {
            var newToken = new AccessToken()
            {
                Key = _passwordHasher.Hash(deviceName),
                DeviceName = deviceName,
                DeviceIp = deviceIp,
                Roles = new UserRole[] { UserRole.Guest }
            };

            return await _accessTokenRepository.Add(newToken);
        }
        public async Task<User> CreateUser(string username, string password, string phone, AccessToken token)
        {
            if (token.ExpireDate < DateTime.UtcNow) { throw new ArgumentException(nameof(token)); }
            var newUser = new User
            {
                Name = username,
                Password = _passwordHasher.Hash(password),
                Phone = phone
            };
            var result = await _userRepository.Add(newUser);
			if (newUser.Id != 0)
			{
				token.Roles = new UserRole[] { UserRole.Guest, UserRole.User };
				token.ExpireDate = DateTime.UtcNow.AddMinutes(30);
				token.UserId = newUser.Id;
				await _accessTokenRepository.Update(token);
			}
			return result;
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
