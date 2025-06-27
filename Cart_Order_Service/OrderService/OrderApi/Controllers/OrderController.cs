using Microsoft.AspNetCore.Mvc;
using OrderService.BLL.Abstractions;
using OrderService.BLL.Dto;
using OrderService.BLL.Dto.Order;
using OrderService.Domain.Enums;
using SupperBackEnd.Dto;

namespace OrderApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IOrderService orderService) : ControllerBase()
    {
        private readonly IOrderService _orderService = orderService;

        /// <summary>
        /// Создать заказ
        /// </summary>
        [HttpPost("AddProduct")]
        public async Task<ActionResult<string>> AddProduct([FromBody] AddOrderDto dto, CancellationToken cancellationToken)
        {
            AnswerWithBackendDto<OrderDto> result = new();
            try
            {
                result = await _orderService.AddOrderAsync(dto, cancellationToken);

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
        /// Удалить заказ
        /// </summary>
        [HttpDelete("DeleteOrder")]
        public async Task<ActionResult<string>> DeleteOrder([FromBody] DeleteOrderDto dto, CancellationToken cancellationToken)
        {
            AnswerWithBackendDto<OrderDto> result = new();
            try
            {
                 result = await _orderService.DeleteOrderAsync(dto, cancellationToken);

                if (!result.DataReceived)
                {
                    return BadRequest(result.ErrorLog);
                }

                return Ok($"Заказ удален. Товары перенесены в корзину");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Получить все заказы пользователя
        /// </summary>
        [HttpGet("GetAllOrderUser")]
        public async Task<ActionResult<string>> GetAllOrderUser([FromBody] GetAllOrderDto dto, CancellationToken cancellationToken)
        {
            AnswerWithBackendDto<OrderDto> result = new();
            try
            {
                result = await _orderService.GetAllOrderUserAsync(dto, cancellationToken);

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
        /// Получить заказ пользователя
        /// </summary>
        [HttpGet("GetOrderUser")]
        public async Task<ActionResult<string>> GetOrderUser([FromBody] GetOrderDto dto, CancellationToken cancellationToken)
        {
            AnswerWithBackendDto<OrderDto> result = new();
            try
            {
                result = await _orderService.GetOrderUserAsync(dto, cancellationToken);

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
        /// Обновить статус текущего заказа
        /// </summary>
        [HttpDelete("UpdateOrderStatus")]
        public async Task<ActionResult<int>> UpdateOrderStatus([FromBody] UpdateOrderDto dto, CancellationToken cancellationToken)
        {
            AnswerWithBackendDto<OrderDto> result = new();
            try
            {
                result = await _orderService.UpdateOrderStatusAsync(dto, cancellationToken);

                if (!result.DataReceived)
                {
                    return BadRequest(result.ErrorLog);
                }

                return Ok(result.DataReceived);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}