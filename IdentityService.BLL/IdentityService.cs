using BLL.Extensions;
using BLL.Identity.Dto;
using IdentityService.BLL.Abstractions;
using IdentityService.BLL.Abstractions.Utilities;
using IdentityService.BLL.Exceptions;
using IdentityService.DAL.Abstractions;
using IdentityService.Domain;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace IdentityService.BLL
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository<User> _repository;
        private readonly IShopOwnerService<ShopOwner> _shopOwnerService;
        private readonly IJwtTokenProvider _jwtTokenProvider;
        private readonly IHasher _hasher;
        private readonly ILogger _logger;
        public IdentityService(IUserRepository<User> repository,
            ILogger<IdentityService> logger,
            IJwtTokenProvider jwtTokenProvider,
            IHasher hasher)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _jwtTokenProvider = jwtTokenProvider;
            _hasher = hasher;
        }

        public async Task<string> GetGuestToken()
        {
            return _jwtTokenProvider.GenerateToken();
        }
        public async Task<User> GetUserInfo(int userId)
        {
            return await _repository.Get(userId);
        }

        public async Task<User> Register(RegisterUserDto dto)
        {
            if (dto.Contact.IsEmail()) return await RegisterByEmail(dto);
            else if (dto.Contact.IsPhoneNumber()) return await RegisterByPhone(dto);
            else throw new InvalidContactInputException();
        }
        public async Task<bool> RegisterSeller(RegisterShopOwnerDto dto)
        {
            try
            {
                var result = await _shopOwnerService.Add(dto.ToEntity());
                if (result == null || result.Id == 0) return false;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<User> RegisterByPhone(RegisterUserDto dto)
        {
            var entity = dto.ToEntity();
            entity.Password = _hasher.Hash(entity.Password);
            var result = await _repository.Add(entity);
            return result;
        }
        public async Task<User> RegisterByEmail(RegisterUserDto dto)
        {
            var newUser = new User
            {
                Name = dto.Username,
                Password = _hasher.Hash(dto.Password),
                Email = dto.Contact
            };
            var result = await _repository.Add(newUser);
            return result;
        }
        public async Task<bool> ChangePassword(ChangeUserPasswordDto dto)
        {
            return await _repository.ChangePassword(dto.UserId, _hasher.Hash(dto.Password));
        }

        /// <summary>
        /// Метод для аутентификации пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>jwt токен</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidContactInputException"></exception>
        public async Task<string> Login(AuthDto dto)
        {
            if (dto.Contact.IsNullOrEmpty()) throw new ArgumentNullException(nameof(dto.Contact));
            if (dto.Password.IsNullOrEmpty()) throw new ArgumentNullException(nameof(dto.Password));

            if (dto.Contact.IsEmail()) return await AuthUserByEmail(dto);
            else if (dto.Contact.IsPhoneNumber()) return await AuthUserByPhone(dto);
            else throw new InvalidContactInputException();
        }
        public async Task<string> AuthUserByEmail(AuthDto dto)
        {
            var user = await _repository.GetByEmail(dto.Contact);

            if (user is null) throw new InvalidContactInputException();

            var isPasswordValid = _hasher.Verify(dto.Password, user.Password);
            if (isPasswordValid)
            {
                return _jwtTokenProvider.GenerateToken(user);
            }
            else throw new InvalidContactInputException();
        }
        public async Task<string> AuthUserByPhone(AuthDto dto)
        {
            var user = await _repository.GetByPhone(dto.Contact);

            if (user is null) throw new InvalidContactInputException();

            var isPasswordValid = _repository.Verify(dto.Password, user.Password);
            if (isPasswordValid)
            {
                return _jwtTokenProvider.GenerateToken(user);
            }
            else throw new InvalidContactInputException();
        }

        public async Task<string?> BizLogin(AuthDto dto)
        {
            User? user = new User();

            if (dto.Contact.IsEmail()) user = await _repository.GetByEmail(dto.Contact);
            else if (dto.Contact.IsPhoneNumber()) user = await _repository.GetByPhone(dto.Contact);
            else throw new InvalidContactInputException();

            if (user is null) return null;

            bool isPasswordValid = _hasher.Verify(dto.Password, user.Password);
            if (isPasswordValid)
            {
                return await BizLogin(user.Id);
            }
            else return null;
        }
        public async Task<string> BizLogin(string token)
        {
            var claims = _jwtTokenProvider.ValidateToken(token);
            var userIdClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim is null) return null;

            var userId = Convert.ToInt32(userIdClaim.Value);

            return await BizLogin(userId);
        }
        public async Task<string> BizLogin(int userId)
        {
            var shopOwners = await _shopOwnerRepository.GetAllByUserId(userId);
            var user = await _userRepository.Get(userId);
            if (user != null && shopOwners != null)
            {
                return _jwtTokenProvider.GenerateToken(user, shopOwners);
            }
            else return null;
        }
    }
}
