using BLL.Identity.Dto;
using Domain.Entities;

namespace BLL.Identity.Abstractions
{
    public interface IIdentityService
    {
        public Task<string> GetGuestToken();
        public Task<User> Register(RegisterUserDto dto);
        public Task<User> RegisterByPhone(RegisterUserDto dto);
        public Task<User> RegisterByEmail(RegisterUserDto dto);
        public Task<User> GetUserInfo(int userId);
        public Task<bool> RegisterSeller(RegisterShopOwnerDto dto);
        public Task<bool> ChangePassword(ChangeUserPasswordDto dto);
        public Task<string> Login(AuthDto dto);
		public Task<string> AuthUserByEmail(AuthDto dto);
        public Task<string> AuthUserByPhone(AuthDto dto);
        public Task<string> BizLogin(AuthDto dto);
        public Task<string> BizLogin(string token);

	}
}
