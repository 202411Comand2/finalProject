using CartService.BLL.Abstractions;
using CartService.BLL.Dto;
using Microsoft.AspNetCore.Mvc;
using SupperBackEnd.Dto;

namespace CartApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController(ICartService cartService) : ControllerBase()
    {
        private readonly ICartService _cartService = cartService;

        /// <summary>
        /// Добавить товар в корзину
        /// </summary>
        [HttpPost("AddProduct")]
        public async Task<ActionResult<string>> AddProduct([FromQuery] AddCartDto dto, CancellationToken cancellationToken)
        {
            AnswerWithBackendDto<CartDto> result = new();
            try
            {
                result = await _cartService.AddCartProductAsync(dto, cancellationToken);

                if (!result.DataReceived)
                {
                    return BadRequest(result.ErrorLog);
                }

                return Ok($"Создана запись с Id: {result.ObjectDto.Id}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Удалить товар из корзины
        /// </summary>
        [HttpDelete("DeleteProduct")]
        public async Task<ActionResult> DeleteProduct([FromQuery] DeleteCartDto dto, CancellationToken cancellationToken)
        {
            AnswerWithBackendDto<CartDto> result = new();
            try
            {
                result = await _cartService.DeleteProductAsync(dto, cancellationToken);

                if (!result.DataReceived)
                {
                    return BadRequest(result.ErrorLog);
                }

                return Ok($"Товар удален из корзины");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получить корзину пользователя
        /// </summary>
        [HttpGet("GetCartUser")]
        public async Task<ActionResult<string>> GetCartUser([FromQuery] GetCartDto dto, CancellationToken cancellationToken)
        {
            AnswerWithBackendDto<CartDto> result = new();
            try
            {
                result = await _cartService.GetCartUserAsync(dto, cancellationToken);

                if (!result.DataReceived)
                {
                    return BadRequest(result.ErrorLog);
                }

                return result.GetCollectionNotProblem();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновить количество товара в корзину
        /// </summary>
        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<string>> UpdateProduct([FromQuery] UpdateCartDto dto, CancellationToken cancellationToken)
        {
            AnswerWithBackendDto<CartDto> result = new();
            try
            {
                result = await _cartService.UpdateProductAsync(dto, cancellationToken);

                if (!result.DataReceived)
                {
                    return BadRequest(result.ErrorLog);
                }

                return Ok($"Количество обновлено, Id: {result.ObjectDto.Id}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
