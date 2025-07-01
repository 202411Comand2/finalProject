using CommentService.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupperBackEnd.Dto;
using System.Security.Claims;

namespace API.Controllers.Product
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController(ICommentMainService commentService, ILogger<CommentController> logger) : ControllerBase()
    {
        ICommentMainService _commentService = commentService;
        ILogger _logger = logger;

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            return Ok($"Product {id}");
        }

        /// <summary>
        /// Добавить новый рейтинг
        /// </summary>
        [HttpPost("Add")]
        [Authorize]
        public async Task<ActionResult<int>> Add([FromBody] AddCommentDto Dto)
        {
            var username = User.Identity?.Name;
            var nameIdentifier = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (nameIdentifier != null)
            {
                int temp = -1;
                var isParsed = int.TryParse(nameIdentifier, out temp);
                if (temp >= 0 && isParsed) Dto.UserId = temp;
                else return BadRequest("Unable to read user id from token");
            }
            else return BadRequest("Unable to read user id from token");
            if (username != null)
            {
                Dto.UserName = username;
                _logger.LogInformation($"Никнейм был корректно извлечён из jwt ({username})");
            }
            else _logger.LogInformation("Что-то пошло не так при извлечении никнейма из jwt");

            AnswerWithBackendDto<CommentDto> result = new();
            try
            {
                result = await _commentService.AddNewComment(Dto);
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
        /// Обновление комментария
        /// </summary>
        [HttpPut("Update")]
        [Authorize]
        public async Task<ActionResult<string>> UpdateComment([FromBody] UpdateCommentDto Dto)
        {
            AnswerWithBackendDto<CommentDto> result = new();
            try
            {
                result = await _commentService.UpdateComment(Dto);
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
        /// Удаление комментарий (скрыть IsDeleted = true)
        /// </summary>
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<ActionResult<int>> Delete([FromBody] DeleteCommentDto Dto)
        {
            AnswerWithBackendDto<CommentDto> result = new();
            try
            {
                result = await _commentService.DeleteComment(Dto);
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
        /// Получить все комментарии по продукту
        /// </summary>
        [HttpGet("GetCommentsProduct")]
        public async Task<ActionResult<string>> GetCommentsProduct([FromQuery] GetAllCommentsProduct Dto)
        {
            AnswerWithBackendDto<CommentDto> result = new();
            try
            {
                result = await _commentService.GetCommentProduct(Dto);
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
