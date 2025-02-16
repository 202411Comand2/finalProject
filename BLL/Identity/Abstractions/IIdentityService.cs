using Domain.Entities;

namespace BLL.Identity.Abstractions
{
    public interface IIdentityService
    {
        public Task<string> GetGuestToken();
        public Task<User> Register(string username, string password, string contact);
        public Task<User> RegisterByPhone(string username, string password, string phone);
        public Task<User> RegisterByEmail(string username, string password, string email);
        public Task<bool> RegisterSeller(string token, int shopId);
        public Task<bool> RegisterManager(int userId, int shopId);
        public Task ChangePassword(string token, string newPassword);
        public Task<string> Login(string contact, string password);
		public Task<string> AuthUserByEmail(string email, string password);
        public Task<string> AuthUserByPhone(string phone, string password);
        public Task<string> BizLogin(string contact, string password);
        public Task<string> BizLogin(string token);

	}
}
