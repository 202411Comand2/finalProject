using ManagersShopsService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SupperBackEnd.Dto;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ManagersShopsService.BLL.Dto;
using SupperBackEnd.User;
using SupperBackEnd.ServerResponseEND;

namespace ManagersShopsApi.Controllers
{
    [ApiController]
    [Route("api/Owner")]
    public class ManagersShopsController : ControllerBase
    {
        public ManagersShopsController(IConfiguration configuration, IManagersShopsMainService managersShopsMainService)
        {
            _configuration = configuration;
            _managersShopsMainService = managersShopsMainService;
        }
        private readonly IConfiguration _configuration;
        private readonly IManagersShopsMainService _managersShopsMainService;




        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok($"Auth {id}");


        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddManagersShopsDto")]
        public async Task<IActionResult> AddShopManeger([FromBody] AddManagersShopsDto model)
        {
            AnswerWithBackendDto<AddManagersShopsDto> result = new();
            Console.WriteLine($"Зашёл в сервис Добавления пользователей в магазин\nUserId {model.UserId} RoleUser {model.RoleUser}");
            try
            {
                // Получаем токен из заголовка
                int idUser = ValidationToken(Request.Headers["Authorization"].FirstOrDefault());
                Console.WriteLine($"Получаем данные из токена id {idUser}");
                if (idUser > 0) 
                {
                    model.UserId = idUser;
                }
                try
                {
                    switch (idUser)
                    {
                        case -2: //токен просрочен
                            Console.WriteLine("Token is missing");
                            return Unauthorized(new ApiResponse<AddManagersShopsDto>(false, null, "Token is missing"));
                        case -3:
                            Console.WriteLine("Invalid token claims");
                            return Unauthorized(new ApiResponse<AddManagersShopsDto>(false, null, "Invalid token claims"));
                        default:
                            Console.WriteLine("Пользователь был добавен в магазин");
                            model.UserId = idUser;
                            var userInfo = await _managersShopsMainService.AddManagersShopsDto(model);
                            if (userInfo.DataReceived == true)
                            {
                                return Ok(new ApiResponse<AddManagersShopsDto>(true, userInfo.ObjectDto, null));
                            }
                            else
                            {
                                return BadRequest(new ApiResponse<AddManagersShopsDto>(false, null, userInfo.ErrorLog));
                            }
                    }
                }
                catch (SecurityTokenException ex)
                {
                    return Unauthorized(new ApiResponse<AddManagersShopsDto>(false, null, $"Invalid token: {ex.Message}"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<AddManagersShopsDto>(false, null, ex.Message));
            }
        }

        /// <summary>
        /// Получить магазины пользователя (упращённая версия
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("GetShop")]
        public async Task<ActionResult<string>> GetShop() 
        {
            AnswerWithBackendDto<AddManagersShopsDto> result = new();
            try
            {
                // Получаем токен из заголовка
                int idUser = ValidationToken(Request.Headers["Authorization"].FirstOrDefault());
                Console.WriteLine($"Получаем данные из токена id {idUser}");
                try
                {
                    switch (idUser)
                    {
                        case -2: // токен отсутвует
                            Console.WriteLine("Token is missing");
                            return Unauthorized("Token is missing");
                        case -3: // Недействительные заявки на токены
                            Console.WriteLine("Invalid token claims");
                            return Unauthorized("Invalid token claims");
                        default:
                            Console.WriteLine("Возращаю список магазинов");
                           
                          

                            var userInfo = await _managersShopsMainService.GetShop(idUser);
                            if (userInfo.DataReceived == true)
                            {
                                return Ok(userInfo.GetCollectionNotProblem());
                            }
                            else
                            {
                                return BadRequest(userInfo.ErrorLog);
                            }
                    }
                }
                catch (SecurityTokenException ex)
                {
                    return Unauthorized(new ApiResponse<AddManagersShopsDto>(false, null, $"Invalid token: {ex.Message}"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<AddManagersShopsDto>(false, null, ex.Message));
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
        #endregion
    }
}
