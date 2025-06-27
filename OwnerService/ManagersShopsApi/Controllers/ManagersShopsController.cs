using ManagersShopsService;
using ManagersShopsService.BLL.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Platform.DAL.Validatoin;
using SupperBackEnd.Dto;

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
        /// Добавить пользователя Владельца
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("AddOwnerShops")]
        public async Task<IActionResult> AddShopOwner([FromBody] AddManagersShopsDto model)
        {
            AnswerWithBackendDto<AddManagersShopsDto> result = new();
            Console.WriteLine($"Зашёл в сервис Добавления пользователей в магазин\nUserId {model.UserId} RoleUser {model.RoleUser}");
            try
            {
                // Получаем токен из заголовка
                int idUser = new Validation(_configuration).ValidationToken(Request.Headers["Authorization"].FirstOrDefault());
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
        /// Добавить пользователя помошника
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("AddManegerShops")]
        public async Task<IActionResult> AddShopManeger([FromBody] AddManagersShopsDto model)
        {
            AnswerWithBackendDto<AddManagersShopsDto> result = new();
            Console.WriteLine($"Зашёл в сервис Добавления пользователей в магазин\nUserId {model.UserId} RoleUser {model.RoleUser}");
            try
            {
                // Получаем токен из заголовка
                int idUser = new Validation(_configuration).ValidationToken(Request.Headers["Authorization"].FirstOrDefault());
                Console.WriteLine($"Получаем данные из токена id {idUser}");
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
        [Authorize]
        [HttpGet("GetShop")]
        public async Task<ActionResult<string>> GetShop() 
        {
            try
            {
                // Получаем токен из заголовка
                int idUser = new Validation(_configuration).ValidationToken(Request.Headers["Authorization"].FirstOrDefault());
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

        [Authorize] 
        [HttpGet("GetManager")]
        public async Task<ActionResult> GetManager([FromQuery] int idShop) 
        {
            try
            {
                int idUser = new Validation(_configuration).ValidationToken(Request.Headers["Authorization"].FirstOrDefault());
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
                            Console.WriteLine("Возращаю список менеджеров");
                            var userInfo = await _managersShopsMainService.GetManagersShopsDto(idShop);
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


        [Authorize]
        [HttpDelete("DeleteManager")]
        public async Task<ActionResult> DeleteManager([FromBody] DeleteManagersShopsDto model) 
        {
            Console.WriteLine($"Удаление менеджера");
            try
            {
                int idUser = new Validation(_configuration).ValidationToken(Request.Headers["Authorization"].FirstOrDefault());
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
                            Console.WriteLine("Возращаю список менеджеров");
                            var userInfo = await _managersShopsMainService.DeleteManagersShopsDto(model);
                            if (userInfo.DataReceived == true)
                            {
                                return Ok();
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

    }
}
