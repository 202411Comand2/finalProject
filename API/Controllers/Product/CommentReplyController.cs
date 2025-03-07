using BLL.Dto.Comment;
using BLL.Dto.ReplyComment;
using BLL.Products;
using BLL.Products.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Product
{
    [ApiController]
    [Route("[controller]")]
    public class CommentReplyController(ICommentReplyService commentReplyService) : ControllerBase()
    {

        ICommentReplyService _commentReplyService = commentReplyService;

        /// <summary>
        /// Добавить ответ на комментарий пользователя со стороны магазина (id комментария пользователя)
        /// </summary>
        [HttpPost("Add")]
        public async Task<ActionResult<int>> Add([FromBody] AddReplyCommentDto Dto)
        {
            int result = -1;
            try
            {
                result = await _commentReplyService.AddReplyComment(Dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (result==-1)
            {
                return NotFound(-1);
            }
            return Ok(result);
        }

        /// <summary>
        /// Обновить ответ на комментарий пользователя со стороны магазина (id комментария ответного)
        /// </summary>
        [HttpPut("Update")]
        public async Task<ActionResult<int>> Update([FromBody] UpdateCommentReplyDto Dto)
        {
            bool result = false;
            try
            {
                result = await _commentReplyService.UpdateCommentReply(Dto);
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
        /// Удалить (скрыть IsDeleted = true) ответ на комментарий пользователя со стороны магазина 
        /// </summary>
        [HttpDelete("Delete")]
        public async Task<ActionResult<int>> Delete([FromBody] DeleteCommentReplyDto Dto)
        {
            bool result = false;
            try
            {
                result = await _commentReplyService.DeleteCommentReply(Dto);
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

    }
}
       


    



      
    



  