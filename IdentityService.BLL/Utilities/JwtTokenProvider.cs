using IdentityService.BLL.Abstractions.Utilities;
using IdentityService.BLL.Options;
using IdentityService.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.BLL.Utilities
{
    public class JwtTokenProvider : IJwtTokenProvider
    {
        private readonly JwtOptions _options;
        private readonly ILogger _logger;
        public JwtTokenProvider(IOptions<JwtOptions> options,
            ILogger<JwtTokenProvider> logger)
        {
            _logger = logger;
            _options = options.Value;
        }
        public string GenerateToken()
        {
            _logger.LogDebug("New guest token was generated");
            throw new NotImplementedException();
        }

        public string GenerateToken(User user)
        {
            _logger.LogDebug("New jwt was generated");
            throw new NotImplementedException();
        }

        private string GenerateToken(Claim[] claims, int expMinutes = 120)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddMinutes(expMinutes)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
