using CommentService.BLL;
using Microsoft.AspNetCore.Mvc;
using SupperBackEnd.Dto;

namespace API.Controllers.Product
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentReplyController(ICommentReplyService commentReplyService) : ControllerBase()
    {

        ICommentReplyService _commentReplyService = commentReplyService;

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok($"Product {id}");
        /// <summary>
        /// Добавить ответ на комментарий пользователя со стороны магазина (id комментария пользователя)
        /// </summary>
        [HttpPost("Add")]
        public async Task<ActionResult<int>> Add([FromBody] AddReplyCommentDto Dto)
        {
            AnswerWithBackendDto<ReplyCommentDto> result = new();
            try
            {
                result = await _commentReplyService.AddReplyComment(Dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            if (!result.DataReceived)
            {
                return BadRequest(result.ErrorLog);
            }
            return Ok(result.ObjectDto.Id);// true
        }

        /// <summary>
        /// Обновить ответ на комментарий пользователя со стороны магазина (id комментария ответного)
        /// </summary>
        [HttpPut("Update")]
        public async Task<ActionResult<int>> Update([FromBody] UpdateCommentReplyDto Dto)
        {
            AnswerWithBackendDto<ReplyCommentDto> result = new();
            try
            {
                result = await _commentReplyService.UpdateCommentReply(Dto);
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
        /// Удалить (скрыть IsDeleted = true) ответ на комментарий пользователя со стороны магазина 
        /// </summary>
        [HttpDelete("Delete")]
        public async Task<ActionResult<int>> Delete([FromBody] DeleteCommentReplyDto Dto)
        {
            AnswerWithBackendDto<ReplyCommentDto> result = new();
            try
            {
                result = await _commentReplyService.DeleteCommentReply(Dto);
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
       


    



      
    



  