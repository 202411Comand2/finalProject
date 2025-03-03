using BLL.Identity.Abstractions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL.Identity
{
    public class JwtTokenProvider(IOptions<JwtOptions> options) : IJwtTokenProvider
	{
		private readonly JwtOptions _options = options.Value;
		/// <summary>
		/// Генерирует jwt токен
		/// </summary>
		/// <param name="user"></param>
		/// <param name="shopCredentials"></param>
		/// <returns></returns>
		public string GenerateToken(User user, IList<ShopOwner> shopCredentials = null)
		{
			if (shopCredentials != null)
			{
				var claims = new List<Claim>(3 + shopCredentials.Count);
				claims.Add(new Claim(_options.UsernameClaimName, user.Name));
				claims.Add(new Claim(_options.UserIdClaimName, user.Id.ToString()));
				claims.Add(new Claim(_options.UserRoleClaimName, UserRole.User.ToString()));
				foreach(var cred in shopCredentials)
				{
					if(cred.IsHost)
						claims.Add(new Claim("shopAccess", $"{cred.Id};{cred.ShopId};{UserRole.ShopOwner}"));
					else
						claims.Add(new Claim("shopAccess", $"{cred.Id};{cred.ShopId};{UserRole.ShopManager}"));
				}
				return GenerateToken(claims.ToArray(), 10);
			}
			else return GenerateToken(user, UserRole.User);
		}
		public string GenerateToken(User user, UserRole role)
		{
			Claim[] claims = [new(_options.UserIdClaimName, user.Id.ToString()),
				new(_options.UserRoleClaimName, role.ToString())];

			return GenerateToken(claims);
		}

		public string GenerateToken()
		{
			Claim[] claims = [new (_options.UserRoleClaimName, UserRole.Guest.ToString())];

			return GenerateToken(claims);
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

		public IEnumerable<Claim> ValidateToken(string tokenValue)
		{
			var handler = new JwtSecurityTokenHandler();
			var valParams = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateLifetime = true,
			};

			try
			{
				var principal = handler.ValidateToken(tokenValue, valParams, out var validatedToken);

				return principal.Claims;
			}
			catch (SecurityTokenException ex)
			{
				// Добавить лог об ошибке
				return null;
			}
		}
	}
}
