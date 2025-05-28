using BLL.Identity.Dto;
using IdentityService.Domain;

namespace IdentityService.BLL.Abstractions
{
    public interface IIdentityService
    {
        public string GetGuestToken();
        public Task<User> GetUserInfo(int userId, CancellationToken? cancellationToken = null);
        public Task<User> Register(RegisterUserDto dto, CancellationToken? cancellationToken = null);
        public Task<bool> RegisterSeller(RegisterShopOwnerDto dto, CancellationToken? cancellationToken = null);
        public Task<User> RegisterByPhone(RegisterUserDto dto, CancellationToken? cancellationToken = null);
        public Task<User> RegisterByEmail(RegisterUserDto dto, CancellationToken? cancellationToken = null);
        public Task<bool> ChangePassword(ChangeUserPasswordDto dto, CancellationToken? cancellationToken = null);

        /// <summary>
        /// Aутентификация пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>jwt токен</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidContactInputException"></exception>
        public Task<string> Login(AuthDto dto, CancellationToken? cancellationToken = null);
        public Task<string?> BizLogin(AuthDto dto, CancellationToken? cancellationToken = null);
        public Task<string> BizLogin(string token, CancellationToken? cancellationToken = null);
    }
}
