using BLL.Abstractions;
using BLL.Dto;
using Microsoft.AspNetCore.Mvc;
using SupperBackEndDto;
using System.Net;

namespace CartApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.NotFound)]
    public class CartController(ICartService cartService) : ControllerBase()
    {
        private readonly ICartService _cartService = cartService;

        /// <summary>
        /// Добавить товар в корзину/обновить количество
        /// </summary>
        [HttpPost("AddProduct")]
        [ProducesResponseType<int>((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> AddProduct([FromQuery] AddCartDto dto, CancellationToken cancellationToken)
        {
            AnswerWithBackendDto<CartDto> result = new();
            try
            {
                result = await _cartService.AddCartProduct(dto, cancellationToken);
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

            /*
             public async Task<Results<Ok<FlsCardDataDto>, ProblemHttpResult>>
            public async Task<Results<Ok, Created, ProblemHttpResult>>
             * 
           if (fls is null)
            {
                return TypedResults.Problem($"ФЛС с ид. {query.FlsId} не найден", statusCode: (int)HttpStatusCode.NotFound);
            }
            catch (ArgumentException ex)
            {
                return TypedResults.Problem(ex.Message, statusCode: (int)HttpStatusCode.BadRequest);
            }
            return TypedResults.Ok(fls);

            При создании/изменении
            
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
            return dkp.Id is null ? TypedResults.Created() : TypedResults.Ok();
            return TypedResults.Created();
             * */
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
