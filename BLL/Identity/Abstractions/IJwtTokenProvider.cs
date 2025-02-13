using Domain.Entities;
using Domain.Enums;

namespace BLL.Identity.Abstractions
{
    public interface IJwtTokenProvider
    {
        public string GenerateToken(User user, UserRole role);
        public string GenerateToken();
    }
}
