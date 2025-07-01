using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OrderService.BLL.Abstractions;
using OrderService.BLL.Dto.Order;
using Platform.DAL.Validatoin;
using SupperBackEnd.Dto;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("api/Order")]
    public class OrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOrderService _orderService;

        public OrderController(IConfiguration configuration, IOrderService orderService)
        {
            _configuration = configuration;
            _orderService = orderService;
        }

        /// <summary>
        /// Создать заказ
        /// </summary>
        [Authorize]
        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder([FromBody] AddOrderDto dto) //, CancellationToken cancellationToken)
        {
            try
            {
                // Получаем токен из заголовка
                int idUser = new Validation(_configuration).ValidationToken(Request.Headers["Authorization"].FirstOrDefault());

                if (idUser > 0)
                {
                    dto.UserId = idUser;
                }

                try
                {
                    switch (idUser)
                    {
                        case -2: //токен просрочен
                            return Unauthorized(new ApiResponse<AddOrderDto>(false, null, "Token is missing"));
                        case -3:
                            return Unauthorized(new ApiResponse<AddOrderDto>(false, null, "Invalid token claims"));
                        default:
                            dto.UserId = idUser;
                            var userInfo = await _orderService.AddOrder(dto);//, cancellationToken);

                            if (userInfo.DataReceived == true)
                            {
                                return Ok(new ApiResponse<AddOrderDto>(true, userInfo.ObjectDto, null));
                            }
                            else
                            {
                                return BadRequest(new ApiResponse<AddOrderDto>(false, null, userInfo.ErrorLog));
                            }
                    }
                }
                catch (SecurityTokenException ex)
                {
                    return Unauthorized(new ApiResponse<AddOrderDto>(false, null, $"Invalid token: {ex.Message}"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<AddOrderDto>(false, null, ex.Message));
            }
        }

        /// <summary>
        /// Удалить товар из заказа
        /// </summary>
        [Authorize]
        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder([FromBody] DeleteOrderDto dto, CancellationToken cancellationToken)
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
                        case -2: //токен просрочен
                            Console.WriteLine("Token is missing");
                            return Unauthorized(new ApiResponse<DeleteOrderDto>(false, null, "Token is missing"));
                        case -3:
                            Console.WriteLine("Invalid token claims");
                            return Unauthorized(new ApiResponse<DeleteOrderDto>(false, null, "Invalid token claims"));
                        default:
                            Console.WriteLine("Пользователь был добавен в магазин");
                            var result = await _orderService.DeleteOrderAsync(dto, cancellationToken);

                            if (result.DataReceived == true)
                            {
                                return Ok(new ApiResponse<DeleteOrderDto>(true, result.ObjectDto, null));
                            }
                            else
                            {
                                return BadRequest(new ApiResponse<DeleteOrderDto>(false, null, result.ErrorLog));
                            }
                    }
                }
                catch (SecurityTokenException ex)
                {
                    return Unauthorized(new ApiResponse<DeleteOrderDto>(false, null, $"Invalid token: {ex.Message}"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<DeleteOrderDto>(false, null, ex.Message));
            }
        }
        
        /// <summary>
        /// Получить все заказы пользователя
        /// </summary>
        [Authorize]
        [HttpGet("GetAllOrderUser")]
        public async Task<IActionResult> GetAllOrderUser()//CancellationToken cancellationToken)
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
                            return Unauthorized("Token is missing");
                        case -3:
                            return Unauthorized("Invalid token claims");
                        default:
                            var userInfo = await _orderService.GetAllOrderUserAsync(idUser); //, cancellationToken);
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
                    return Unauthorized($"Invalid token: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Получить заказ пользователя
        /// </summary>
        [Authorize]
        [HttpGet("GetOrderUser")]
        public async Task<IActionResult> GetOrderUser([FromBody] GetOrderDto dto, CancellationToken cancellationToken)
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
                            return Unauthorized(new ApiResponse<GetOrderDto>(false, null, "Token is missing"));
                        case -3:
                            return Unauthorized(new ApiResponse<GetOrderDto>(false, null, "Invalid token claims"));
                        default:
                            var result = await _orderService.GetOrderUserAsync(dto, cancellationToken);

                            if (result.DataReceived == true)
                            {
                                return Ok(new ApiResponse<GetOrderDto>(true, result.ObjectDto, null));
                            }
                            else
                            {
                                return BadRequest(new ApiResponse<GetOrderDto>(false, null, result.ErrorLog));
                            }
                    }
                }
                catch (SecurityTokenException ex)
                {
                    return Unauthorized(new ApiResponse<GetOrderDto>(false, null, $"Invalid token: {ex.Message}"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<GetOrderDto>(false, null, ex.Message));
            }
        }

        /// <summary>
        /// Обновить статус текущего заказа
        /// </summary>
        [Authorize]
        [HttpDelete("UpdateOrderStatus")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] UpdateOrderDto dto)//, CancellationToken cancellationToken)
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
                            return Unauthorized(new ApiResponse<UpdateOrderDto>(false, null, "Token is missing"));
                        case -3:
                            return Unauthorized(new ApiResponse<UpdateOrderDto>(false, null, "Invalid token claims"));
                        default:
                            var result = await _orderService.UpdateOrderStatusAsync(dto); //, cancellationToken);

                            if (result.DataReceived == true)
                            {
                                return Ok(new ApiResponse<UpdateOrderDto>(true, result.ObjectDto, null));
                            }
                            else
                            {
                                return BadRequest(new ApiResponse<UpdateOrderDto>(false, null, result.ErrorLog));
                            }
                    }
                }
                catch (SecurityTokenException ex)
                {
                    return Unauthorized(new ApiResponse<UpdateOrderDto>(false, null, $"Invalid token: {ex.Message}"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<UpdateOrderDto>(false, null, ex.Message));
            }
        }
    }
}