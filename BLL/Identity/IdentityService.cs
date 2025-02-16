using BLL.Identity.Abstractions;
using BLL.Identity.Exceptions;
using BLL.Identity.Extensions;
using DAL.Abstractions;
using DAL.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserRepository _userRepository;
        private readonly ShopOwnerRepository _shopOwnerRepository;
        private readonly AccessTokenRepository _accessTokenRepository;
        private readonly IGenericDataHasher<string> _passwordHasher;
        private readonly IJwtTokenProvider _jwtTokenProvider;

        public IdentityService(
            IContextManager contextManager, 
            IJwtTokenProvider tokenProvider,
            IGenericDataHasher<string> passwordHasher)
        {
            _shopOwnerRepository = new ShopOwnerRepository(contextManager);
            _userRepository = new UserRepository(contextManager);
            _accessTokenRepository = new AccessTokenRepository(contextManager);
            _passwordHasher = passwordHasher;
            _jwtTokenProvider = tokenProvider;
        }

        public async Task<string> GetGuestToken()
        {
            return _jwtTokenProvider.GenerateToken();
        }
        public async Task<User> Register(string username, string password, string contact)
        {
            if (contact.IsEmail()) return await RegisterByEmail(username, password, contact);
            else if (contact.IsPhoneNumber()) return await RegisterByPhone(username, password, contact);
            else throw new InvalidContactInputException();
        }
        public async Task<bool> RegisterSeller(User user, Shop shop)
        {
            var newOwner = new ShopOwner()
            {
                IsHost = true,
                IsDeleted = false,
                UserId = user.Id,
                ShopId = shop.Id,
            };

            var result = await _shopOwnerRepository.Add(newOwner);

            if (result == null) return false;
            return true;
        }
        public async Task<User> RegisterByPhone(string username, string password, string phone)
        {
            var newUser = new User
            {
                Name = username,
                Password = _passwordHasher.Hash(password),
                Phone = phone
            };
            var result = await _userRepository.Add(newUser);
			return result;
		}
        public async Task<User> RegisterByEmail(string username, string password, string email)
        {
            var newUser = new User
            {
                Name = username,
                Password = _passwordHasher.Hash(password),
                Email = email
            };
            var result = await _userRepository.Add(newUser);
            return result;
        }

		public async Task UpdateUser(User user)
        {
			throw new NotImplementedException("Пока не сделал");
		}
        public async Task ChangePassword(string token, string newPassword)
        {
            throw new NotImplementedException("Пока не сделал");
		}
        public async Task<string> Login(string contact, string password)
        {
            if (contact.IsEmail()) return await AuthUserByEmail(contact, password);
            else if (contact.IsPhoneNumber()) return await AuthUserByPhone(contact, password);
            else throw new InvalidContactInputException();
        }
        //public async Task<string> BizLogin(string token, int shopId)
        //{
        //    var claims = _jwtTokenProvider.ValidateToken(token);
            
        //}
        public async Task<string> AuthUserByEmail(string email, string password)
        {
            if (email.IsNullOrEmpty()) {throw new ArgumentNullException(nameof(email));}
            if (password.IsNullOrEmpty()) {throw new ArgumentNullException(nameof(password));}
            var user = await _userRepository.GetByEmail(email);

            if (user is null) throw new InvalidCredentialsException();

            var isPasswordValid = _passwordHasher.Verify(password, user.Password);
            if (isPasswordValid)
            {
                return _jwtTokenProvider.GenerateToken(user, UserRole.User);
            }
            else throw new InvalidCredentialsException();
        }
        public async Task<string> AuthUserByPhone(string phone, string password)
        {
			if (phone.IsNullOrEmpty()) { throw new ArgumentNullException(nameof(phone)); }
			if (password.IsNullOrEmpty()) { throw new ArgumentNullException(nameof(password)); }
			var user = await _userRepository.GetByPhone(phone);

			if (user is null) throw new InvalidCredentialsException();

			var isPasswordValid = _passwordHasher.Verify(password, user.Password);
			if (isPasswordValid)
			{
				return _jwtTokenProvider.GenerateToken(user, UserRole.User);
			}
			else throw new InvalidCredentialsException();
		}
    }
}
