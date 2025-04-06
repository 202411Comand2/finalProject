using BLL.Dto.Favorite;
using BLL.Products.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Product
{


        [ApiController]
        [Route("[controller]")]
        public class FavoriteController(IFavoriteProductService favoriteProductService) : ControllerBase()
        {
            private readonly IFavoriteProductService _favoriteProductService = favoriteProductService;


        /// <summary>
        /// Добавить товар в избранное
        /// </summary>
        [HttpPost("Add")]
        public async  Task<ActionResult<int>> AddFavorite([FromBody] AddFavoriteDto Dto)
        {
            int result = -1;
            try
            {
                result = await _favoriteProductService.AddFavoriteProduct(Dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (result == - 1)
            {
                return NotFound();
            }
            return Ok(result);
        }

       
        /// <summary>
        /// Удалить товар из избранного
        /// </summary>
        /// <param name="davoriteId">ClusterId избранной позиции</param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ActionResult<int>> Delete([FromBody] DeleteFavoriteDto Dto)
        {
            bool result = false;
            try
            {
                result = await _favoriteProductService.DeleteFavoriteProduct(Dto);
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

        [HttpGet("GetFavoriteUser")]
        public async Task<ActionResult<int>> GetFavoriteUser([FromQuery] GetFavoriteDto Dto)
        {
            List<FavoriteDto> result = new  List<FavoriteDto>();
            try
            {
                result = await _favoriteProductService.GetFavoriteUser(Dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (result.Count()==0)
            {
                return NotFound();
            }
            return Ok(new JsonResult(result));
        }

    }
}
