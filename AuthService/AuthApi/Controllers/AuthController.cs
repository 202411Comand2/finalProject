using AuthService.BLL;
using AuthService.BLL.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SupperBackEnd.Dto;
using SupperBackEnd.ServerResponseEND;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace AuthApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthMainService _authService;
        public AuthController(IConfiguration configuration, IAuthMainService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok($"Auth {id}");
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthUserDto model)
        {

            AnswerWithBackendDto<UserDto> result = new();
            Console.WriteLine($"Зашёл в сервис авторизации\nLogin {model.Login} Password {model.Password}");
            try
            {
                // Получаем настройки JWT из конфигурации
                AnswerWithBackendDto<UserDto>
                user = await _authService.AuthUser(model);

                if (user.ObjectDto is null)
                {
                    Console.WriteLine($"User is null");
                    return Unauthorized(); // Возвращаем 401 если аутентификация не прошла
                }
                var token = CreateToken(user.ObjectDto);

                Console.WriteLine($"token user {token}");
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] AddUserDto model)
        {
            AnswerWithBackendDto<UserDto> result = new();
            Console.WriteLine($"Зашёл в сервис регистрации\nLogin {model.Login} Password {model.Password}");
            try
            {
                AnswerWithBackendDto<UserDto>
                user = await _authService.RegisterUser(model);

                if (user.ObjectDto is null)
                {
                    Console.WriteLine($"Не получилось зарегистрировать пользователя");

                    return BadRequest(user.ErrorLog); // Возвращаем 401 если аутентификация не прошла
                }
                var token = CreateToken(user.ObjectDto);
                Console.WriteLine($"Токен получен {token}");

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получить упрощённую информацию о плльзователе
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]  // Только этот метод доступен без токена
        [HttpGet("GetSerchUser")]
        public async Task<IActionResult> GetSerchUser([FromQuery] int id)
        {
            try
            {
                var userInfo = await _authService.GetInfoEasyUser(id);
                if (userInfo.DataReceived)
                {
                    return Ok(userInfo.ObjectDto);
                }
                else
                { return BadRequest(); }
            }
            catch
            {
                return Unauthorized();
            }
        }



        /// <summary>
        /// получения информация и пользователе после авторизации
        /// Работает с токеном jwt
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetUserId")]
        public async Task<ActionResult<string>> GetUserId()
        {
            try
            {
                // Получаем токен из заголовка
                int idUser = ValidationToken(Request.Headers["Authorization"].FirstOrDefault());
                try
                {
                    switch (idUser)
                    {
                        case -2: //токен просрочен
                            return Unauthorized("Token is missing");
                        case -3:
                            return Unauthorized("Invalid token claims");
                        default:
                            var userInfo = await _authService.GetInfoUser(idUser);
                            return Ok(userInfo.ObjectDto);
                    }
                }
                catch (SecurityTokenException ex)
                {
                    return Unauthorized($"Invalid token: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<AuthUserDto>(false, null, ex.Message));
            }
        }


        [HttpPost("Update")]
        public async Task<ActionResult<string>> UpdateInfoUser(UpdateUserDto model)
        {

            try
            {
                Console.WriteLine("Пользователь зашёл в изменения профиля");
                // Получаем токен из заголовка
                int idUser = ValidationToken(Request.Headers["Authorization"].FirstOrDefault());
                try
                {
                    switch (idUser)
                    {
                        case -2: //токен просрочен
                            Console.WriteLine("Token is missing");
                            return Unauthorized(new ApiResponse<AuthUserDto>(false, null, "Token is missing"));
                        case -3:
                            Console.WriteLine("Invalid token claims");
                            return Unauthorized(new ApiResponse<AuthUserDto>(false, null, "Invalid token claims"));
                        default:
                            Console.WriteLine("Данные обновлены!");
                            model.Id = idUser;
                            var userInfo = await _authService.UpdateInfoUser(model);
                            if (userInfo.DataReceived == true)
                            {
                                return Ok(new ApiResponse<UserDto>(true, userInfo.ObjectDto, null));
                            }
                            else
                            {
                                return BadRequest(new ApiResponse<AuthUserDto>(false, null, userInfo.ErrorLog));
                            }
                    }
                }
                catch (SecurityTokenException ex)
                {
                    return Unauthorized(new ApiResponse<AuthUserDto>(false, null, $"Invalid token: {ex.Message}"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<AuthUserDto>(false, null, ex.Message));
            }
        }

        #region вспомогательные ф-и


        /// <summary>
        /// Проверка токена от frontEnd
        /// </summary>
        /// <param name="token"></param>
        /// <returns>-1 означает, что валидация не была пройдена</returns>
        private int ValidationToken(string authHeader)
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


        /// <summary>
        /// Генерация токена для доступа к данным на стороне fronEnd
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private JwtSecurityToken CreateToken(UserDto user)
        {
            // Получаем настройки JWT из конфигурации
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Role, "role admin")
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);
            return token;
        }

        #endregion
    }
}
