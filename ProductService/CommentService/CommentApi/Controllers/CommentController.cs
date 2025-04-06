using BLL.Dto.Comment;
using BLL.Products.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Product
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController(ICommentService commentService) : ControllerBase()
    {

        ICommentService _commentService = commentService;

        /// <summary>
        /// Добавить новый рейтинг
        /// </summary>
        [HttpPost("Add")]
        public async Task<ActionResult<int>> Add([FromBody] AddCommentDto Dto)
        {
            int result = -1;
            try
            {
                result = await _commentService.AddNewComment(Dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (result ==-1)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Обновление комментария
        /// </summary>
        [HttpPut("Update")]
        public async Task<ActionResult<int>> UpdateComment([FromBody] UpdateCommentDto Dto) 
        {
            bool result = false;
            try
            {
                result = await _commentService.UpdateComment(Dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
            
        }

        /// <summary>
        /// Удаление комментарий (скрыть IsDeleted = true)
        /// </summary>
        [HttpDelete("Delete")]
        public async Task<ActionResult<int>> Delete([FromBody] DeleteCommentDto Dto)
        {
            bool result = false;
            try
            {
                result = await _commentService.DeleteComment(Dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result)
            {
                return NotFound();
            }
            return Ok(true);
        }

        /// <summary>
        /// Получить все комментарии по продукту
        /// </summary>
        [HttpGet("GetCommentsProduct")]
        public async Task<ActionResult> GetCommentsProduct([FromQuery] GetAllCommentsProduct Dto)
        {
            List<CommentDto> result = new List<CommentDto>();
            try
            {
                result = await _commentService.GetCommentProduct(Dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (result.Count == 0)
            {
                return NotFound();
            }
            return new JsonResult(result);
        }

    }
}
