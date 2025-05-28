using IdentityService.Domain;

namespace IdentityService.BLL.Abstractions.Utilities
{
    public interface IJwtTokenProvider
    {
        public string GenerateToken();
        public string GenerateToken(User user);
    }
}
