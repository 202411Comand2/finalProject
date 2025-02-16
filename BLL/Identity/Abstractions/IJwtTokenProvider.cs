using Domain.Entities;
using Domain.Enums;
using System.Security.Claims;

namespace BLL.Identity.Abstractions
{
    public interface IJwtTokenProvider
    {
        public string GenerateToken(User user, UserRole role);
        public string GenerateToken();
        public IEnumerable<Claim> ValidateToken(string tokenValue);

	}
}
