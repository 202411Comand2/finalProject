using BLL.Abstractions;
using BLL.Dto;
using Microsoft.AspNetCore.Mvc;
using SupperBackEndDto;

namespace OrderApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IOrderService orderService, CancellationToken token) : ControllerBase()
    {
        private readonly IOrderService _orderService = orderService;
        private readonly CancellationToken _token = token;

        /// <summary>
        /// Создать заказ
        /// </summary>
        [HttpPost("AddProduct")]
        public async Task<ActionResult<int>> AddProduct([FromQuery] CreateOrderDto dto)
        {
            AnswerWithBackendDto<OrderDto> result = new();
            try
            {
                result = await _orderService.AddOrderProduct(dto, token);
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
            AnswerWithBackendDto<OrderDto> result = new();
            try
            {
                result = await _orderService.DeleteProduct(id);
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
        [HttpGet("GetOrderUser")]
        public async Task<ActionResult<string>> GetOrderUser([FromBody] int userId)
        {
            AnswerWithBackendDto<OrderDto> result = new();
            try
            {
                result = await _orderService.GetOrderUser(userId);
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
