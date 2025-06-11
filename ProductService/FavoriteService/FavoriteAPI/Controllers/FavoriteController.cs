using FavoriteService.BLL;
using Microsoft.AspNetCore.Mvc;
using SupperBackEnd.Dto;

namespace API.Controllers.Product
{


    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteController(IFavoriteProductService favoriteProductService) : ControllerBase()
    {
        private readonly IFavoriteProductService _favoriteProductService = favoriteProductService;


        /// <summary>
        /// Добавить товар в избранное
        /// </summary>
        [HttpPost("Add")]
        public async Task<ActionResult<int>> AddFavorite([FromBody] AddFavoriteDto Dto)
        {
            AnswerWithBackendDto<FavoriteDto> result = new();
            try
            {
                result = await _favoriteProductService.AddFavoriteProduct(Dto);
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
        /// Удалить товар из избранного
        /// </summary>
        /// <param name="davoriteId">ClusterId избранной позиции</param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ActionResult<int>> Delete([FromBody] DeleteFavoriteDto Dto)
        {
            AnswerWithBackendDto<FavoriteDto> result = new();
            try
            {
                result = await _favoriteProductService.DeleteFavoriteProduct(Dto);
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

        [HttpGet("GetFavoriteUser")]
        public async Task<ActionResult<string>> GetFavoriteUser([FromQuery] GetFavoriteDto Dto)
        {
            AnswerWithBackendDto<FavoriteDto> result = new();
            try
            {
                result = await _favoriteProductService.GetFavoriteUser(Dto);
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
