using BLL.Abstractions;
using BLL.Exceptions;
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
        private List<AccessToken> _tokenCache; // Удалить после введения Redis

        public IdentityService(IContextManager contextManager)
        {
            _userRepository = new UserRepository(contextManager);
            _accessTokenRepository = new AccessTokenRepository(contextManager);
            _passwordHasher = new StringHasher();
            _tokenCache = new List<AccessToken>();
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

            var result = await _accessTokenRepository.Add(newToken);
            _tokenCache.Add(result);
            return result;
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

		public async Task UpdateUser(User user)
        {
			throw new NotImplementedException("Пока не сделал");
		}
        public async Task ChangePassword(AccessToken token, string newPassword)
        {
            if (!await ValidateToken(token)) throw new InvalidTokenException();

		}
        public async Task<AccessToken> AuthUserByPassword(string email, string password)
		{
			throw new NotImplementedException("Пока не сделал");
		}
		public async Task<AccessToken> AuthUserByEmail(string email, string password)
        {
            throw new NotImplementedException("Пока не сделал");
        }
        public async Task<AccessToken> AuthUserByPhone(string phone, string password)
        {
			throw new NotImplementedException("Пока не сделал");
		}

        private AccessToken FindTokenInCache(int id)
        {
            return _tokenCache.FirstOrDefault(x => x.Id == id);
        }
        private async Task<bool> ValidateToken(AccessToken token)
        {
            if (token == null) return false;
			if (token.ExpireDate < DateTime.UtcNow) { return false; }
			var storedToken = FindTokenInCache(token.Id);
            if (storedToken == null) { storedToken = await _accessTokenRepository.Get(token.Id); }
            if (storedToken == null) { return false; }
            if (storedToken.Key != token.Key) { return false; }
            if (storedToken.ExpireDate != token.ExpireDate) { return false; }
            if (storedToken.DeviceName != token.DeviceName) { return false; }
            return true;
        }
    }
}
