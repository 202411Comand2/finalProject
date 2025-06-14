using Microsoft.AspNetCore.Mvc;
using OrderService.BLL.Abstractions;
using OrderService.BLL.Dto;
using OrderService.Domain.Enums;
using SupperBackEnd.Dto;

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
        public async Task<ActionResult<bool>> AddProduct([FromQuery] CreateOrderDto dto)
        {
            AnswerWithBackendDto<OrderDto> result = new();
            try
            {
                result = await _orderService.AddOrder(dto, _token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result.DataReceived)
            {
                return BadRequest(result.ErrorLog);
            }
            return Ok(true);
        }


        /// <summary>
        /// Обновить статус текущего заказа
        /// </summary>
        [HttpDelete("DeleteProduct")]
        public async Task<ActionResult<int>> DeleteProduct([FromBody] int id)
        {
            AnswerWithBackendDto<OrderDto> result = new();
            try
            {
               // result = await _orderService.UpdateOrderStatus(id, _token);
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
        /// Получить заказы пользователя
        /// </summary>
        [HttpGet("GetOrderUser")]
        public async Task<ActionResult<string>> GetOrderUser([FromBody] int userId)
        {
            AnswerWithBackendDto<OrderDto> result = new();
            try
            {
                result = await _orderService.GetOrderUser(userId, _token);
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

        /// <summary>
        /// Обновить статус текущего заказа
        /// </summary>
        [HttpDelete("UpdateOrderStatus")]
        public async Task<ActionResult<int>> UpdateOrderStatus([FromBody] int id, OrderStatus orderStatus)
        {
            AnswerWithBackendDto<OrderDto> result = new();
            try
            {
                result = await _orderService.UpdateOrderStatus(id, orderStatus, _token);
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
    }
}
