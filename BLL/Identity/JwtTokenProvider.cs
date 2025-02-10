using BLL.Abstractions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace BLL.Identity
{
	public class JwtTokenProvider : IJwtTokenProvider
	{
		public JwtTokenProvider()
		{
		}

		public string GenerateToken(User user, UserRole role)
		{
			var handler = new JwtSecurityTokenHandler();

			var sec = "examplesecret";
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(sec));
			var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var identity = new ClaimsIdentity(new GenericIdentity(user.Email), new[]
			{
				new Claim("id", user.Id.ToString()),
				new Claim("name", user.Name),
				new Claim("role", role.ToString()),
				new Claim("phone", user.Phone)
			});
			var token = handler.CreateJwtSecurityToken(
				subject: identity,
				signingCredentials: signingCredentials,
				audience: "exampleAudience",
				issuer: "exampleIssuer",
				expires: DateTime.UtcNow.AddMinutes(30)
				);
			
			return handler.WriteToken(token);
		}
	}
}
