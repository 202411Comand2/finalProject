using AuthService.BLL;
using AuthService.BLL.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Platform.DAL.Validatoin;
using SupperBackEnd.Dto;
using System.IdentityModel.Tokens.Jwt;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        public AuthController(IConfiguration configuration, IAuthMainService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }
        private readonly IConfiguration _configuration;
        private readonly IAuthMainService _authService;


        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok($"Auth {id}");

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
                var token = new Token(_configuration).CreateToken(user.ObjectDto);

                Console.WriteLine($"token user {token}");
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            catch (Exception ex)
            {
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
                var token = new Token(_configuration).CreateToken(user.ObjectDto);
                Console.WriteLine($"Токен получен {token}");

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            catch (Exception ex)
            {
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
                     return Ok( userInfo.ObjectDto);
                }else
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
                int idUser = new Validation(_configuration).ValidationToken(Request.Headers["Authorization"].FirstOrDefault());
                try
                {
                    switch (idUser)
                    {
                        case -2: //токен просрочен
                            return Unauthorized( "Token is missing");
                        case -3:
                            return Unauthorized( "Invalid token claims");
                        default:
                            var userInfo = await _authService.GetInfoUser(idUser);
                            return Ok(userInfo.ObjectDto);
                    }
                }
                catch (SecurityTokenException ex)
                {
                    return Unauthorized( $"Invalid token: {ex.Message}");
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
                int idUser = new Validation(_configuration).ValidationToken(Request.Headers["Authorization"].FirstOrDefault());
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
    }
}
