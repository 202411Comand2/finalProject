using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Platform.DAL.Validatoin
{
    public class Validation
    {
        private readonly IConfiguration _configuration;

        public Validation(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Проверка токена от frontEnd
        /// </summary>
        /// <param name="token"></param>
        /// <returns>-1 означает, что валидация не была пройдена</returns>
        public int ValidationToken(string authHeader)
        {
            try
            {
                // Получаем настройки JWT из конфигурации
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var secretKey = jwtSettings["SecretKey"];

                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    return -2;
                }

                var token = authHeader.Substring("Bearer ".Length).Trim();

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                // Параметры валидации (должны совпадать с параметрами при генерации токена)
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ValidateIssuerSigningKey = true,

                };

                var handler = new JwtSecurityTokenHandler();
                SecurityToken validatedToken;

                try
                {
                    // Валидация токена
                    var principal = handler.ValidateToken(token, validationParameters, out validatedToken);

                    var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    if (string.IsNullOrEmpty(userId))
                    {
                        return -3;
                    }
                    return Convert.ToInt32(userId);
                }
                catch
                {

                }
            }
            catch
            {
            }
            return -1;
        }
    }
}
