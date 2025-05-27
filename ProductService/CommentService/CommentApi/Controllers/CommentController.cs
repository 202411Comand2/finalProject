using BLL.Dto;
using BLL.Dto.Comment;
using BLL.Products.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Product
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController(ICommentService commentService) : ControllerBase()
    {

        ICommentService _commentService = commentService;

        /// <summary>
        /// Добавить новый рейтинг
        /// </summary>
        [HttpPost("Add")]
        public async Task<ActionResult<int>> Add([FromBody] AddCommentDto Dto)
        {

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
