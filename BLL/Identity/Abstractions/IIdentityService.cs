using Domain.Entities;

namespace BLL.Identity.Abstractions
{
    public interface IIdentityService
    {
        public Task<string> GetGuestToken();
        public Task<User> Register(string username, string password, string contact);
        public Task<User> RegisterByPhone(string username, string password, string phone);
        public Task<User> RegisterByEmail(string username, string password, string email);
        public Task UpdateUser(User user);
        public Task ChangePassword(string token, string newPassword);
        public Task<string> Login(string contact, string password);
		public Task<string> AuthUserByEmail(string email, string password);
        public Task<string> AuthUserByPhone(string phone, string password);
    }
}
