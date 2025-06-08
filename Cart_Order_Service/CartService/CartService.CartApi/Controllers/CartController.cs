using BLL.Abstractions;
using BLL.Dto;
using Microsoft.AspNetCore.Mvc;
using SupperBackEndDto;

namespace CartApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController(ICartService cartService, CancellationToken token) : ControllerBase()
    {
        private readonly ICartService _cartService = cartService;
        private readonly CancellationToken _token = token;

        /// <summary>
        /// Добавить товар в корзину/обновить количество
        /// </summary>
        [HttpPost("AddProduct")]
        public async Task<ActionResult<int>> AddProduct([FromQuery] AddCartDto dto)
        {
            AnswerWithBackendDto<CartDto> result = new();
            try
            {
                result = await _cartService.AddCartProduct(dto, token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result.DataReceived)
            {
                return BadRequest(result.ErrorLog);
            }
            return Ok(result.ObjectDto.Id);
        }


        /// <summary>
        /// Удалить товар из корзины
        /// </summary>
        [HttpDelete("DeleteProduct")]
        public async Task<ActionResult<int>> DeleteProduct([FromBody] int id)
        {
            AnswerWithBackendDto<CartDto> result = new();
            try
            {
                result = await _cartService.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result.DataReceived)
            {
                return BadRequest(result.ErrorLog);
            }
            return Ok(result.DataReceived);
        }

        /// <summary>
        /// Получить корзину пользователя
        /// </summary>
        [HttpGet("GetCartUser")]
        public async Task<ActionResult<string>> GetCartUser([FromBody] int userId)
        {
            AnswerWithBackendDto<CartDto> result = new();
            try
            {
                result = await _cartService.GetCartUser(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result.DataReceived)
            {
                return BadRequest(result.ErrorLog);
            }
            return result.GetCollectionNotProblem();
        }
    }
}
