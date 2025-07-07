using CartService.BLL.Abstractions;
using CartService.BLL.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Platform.DAL.Validatoin;
using SupperBackEnd.Dto;

namespace CartApi.Controllers
{
    [ApiController]
    [Route("api/Cart")]
    public class CartController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICartService _cartService;

        public CartController(IConfiguration configuration, ICartService cartService)
        {
            _configuration = configuration;
            _cartService = cartService;
        }

        /// <summary>
        /// Добавить товар в корзину
        /// </summary>
        //[Authorize]
        [HttpPost("AddCartProduct")]
         public async Task<IActionResult> AddCartProduct([FromBody] AddCartDto dto, CancellationToken cancellationToken)
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
                            return Unauthorized(new ApiResponse<AddCartDto>(false, null, "Token is missing"));
                        case -3:
                            return Unauthorized(new ApiResponse<AddCartDto>(false, null, "Invalid token claims"));
                        default:
                            var userInfo = await _cartService.AddCartProductAsync(dto, cancellationToken);
                           
                            if (userInfo.DataReceived == true)
                            {
                                return Ok(new ApiResponse<CartDto>(true, userInfo.ObjectDto, null));
                            }
                            else
                            {
                                return BadRequest(new ApiResponse<CartDto>(false, null, userInfo.ErrorLog));
                            }
                    }
                }
                catch (SecurityTokenException ex)
                {
                    return Unauthorized(new ApiResponse<CartDto>(false, null, $"Invalid token: {ex.Message}"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<CartDto>(false, null, ex.Message));
            }
        }

        /// <summary>
        /// Удалить товар из корзины
        /// </summary>
        [Authorize]
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromBody] DeleteCartDto dto, CancellationToken cancellationToken)
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
                            return Unauthorized(new ApiResponse<DeleteCartDto>(false, null, "Token is missing"));
                        case -3:
                            return Unauthorized(new ApiResponse<DeleteCartDto>(false, null, "Invalid token claims"));
                        default:
                            var result = await _cartService.DeleteProductAsync(dto, cancellationToken);

                            if (result.DataReceived == true)
                            {
                                return Ok();
                            }
                            else
                            {
                                return BadRequest(result.ErrorLog);
                            }
                    }
                }
                catch (SecurityTokenException ex)
                {
                    return Unauthorized(new ApiResponse<DeleteCartDto>(false, null, $"Invalid token: {ex.Message}"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<DeleteCartDto>(false, null, ex.Message));
            }
        }
        
        /// <summary>
        /// Удалить товар из корзины
        /// </summary>
        [Authorize]
        [HttpDelete("DeleteAllProduct")]
        public async Task<IActionResult> DeleteAllProduct([FromBody] DeleteCartDto dto, CancellationToken cancellationToken)
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
                            return Unauthorized(new ApiResponse<DeleteCartDto>(false, null, "Token is missing"));
                        case -3:
                            return Unauthorized(new ApiResponse<DeleteCartDto>(false, null, "Invalid token claims"));
                        default:
                            var result = await _cartService.DeleteAllProductAsync(dto, cancellationToken);

                            if (result.DataReceived == true)
                            {
                                return Ok();
                            }
                            else
                            {
                                return BadRequest(result.ErrorLog);
                            }
                    }
                }
                catch (SecurityTokenException ex)
                {
                    return Unauthorized(new ApiResponse<DeleteCartDto>(false, null, $"Invalid token: {ex.Message}"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<DeleteCartDto>(false, null, ex.Message));
            }
        }

        /// <summary>
        /// Получить корзину пользователя
        /// </summary>
        [Authorize]
        [HttpGet("GetCartUser")]
        public async Task<ActionResult> GetCartUser(CancellationToken cancellationToken)
        {
            try
            {
                int idUser = new Validation(_configuration).ValidationToken(Request.Headers["Authorization"].FirstOrDefault());
                
                try
                {
                    switch (idUser)
                    {
                        case -2: // токен отсутвует
                            return Unauthorized("Token is missing");
                        case -3: // Недействительные заявки на токены
                            return Unauthorized("Invalid token claims");
                        default:
                            var userInfo = await _cartService.GetCartUserAsync(idUser);
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
                    return Unauthorized(new ApiResponse<GetCartDto>(false, null, $"Invalid token: {ex.Message}"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<GetCartDto>(false, null, ex.Message));
            }
        }

        /// <summary>
        /// Обновить количество товара в корзину
        /// </summary>
        [Authorize]
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateCartDto dto, CancellationToken cancellationToken)
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
                            return Unauthorized(new ApiResponse<CartDto>(false, null, "Token is missing"));
                        case -3:
                            return Unauthorized(new ApiResponse<CartDto>(false, null, "Invalid token claims"));
                        default:
                            var result = await _cartService.UpdateProductAsync(dto, cancellationToken);
                            if (result.DataReceived == true)
                            {
                                return Ok(new ApiResponse<CartDto>(true, result.ObjectDto, null));
                            }
                            else
                            {
                                return BadRequest(new ApiResponse<CartDto>(false, null, result.ErrorLog));
                            }
                    }
                }
                catch (SecurityTokenException ex)
                {
                    return Unauthorized(new ApiResponse<CartDto>(false, null, $"Invalid token: {ex.Message}"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<CartDto>(false, null, ex.Message));
            }
        }
    }
}
