using BLL.Dto.Shop;
using BLL.Products.Abstractions;
using DAL.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Product
{


    [ApiController]
    [Route("[controller]")]
    public class ShopController(IShopService shopService) : ControllerBase()
    {
        private readonly IShopService _shopService = shopService;

        /// <summary>
        /// Добавить магазин
        /// </summary>
        /// <param name="shopDto"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ActionResult<int>> Add([FromBody] AddShopDto shopDto)
        {
            bool result = false;
            try
            {
                result = await _shopService.CreateShop(shopDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result)
            {
                return NotFound();
            }
            return Ok("Магазин создан");
        }


        [HttpDelete("Delete")]
        //  public async Task<ActionResult<int>> Delete([FromBody] ShopDto shopDto)
        public async Task<ActionResult<int>> Delete(DeleteShopDto shopDto)
        {

            bool result = false;
            try
            {
                result = await _shopService.DeleteShop(shopDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result)
            {
                return NotFound();
            }
            return Ok("Магазин был ''удалён''");
        }

        /// <summary>
        /// Обновить магазин
        /// </summary>
        /// <param name="id">ClusterId магазина</param>
        /// <param name="name">Новое название</param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<ActionResult<int>> UpdateName(UpdateShopDto updateShopDto)
        {
            bool result = false;
            try
            {
                result = await _shopService.UpdateNameShop(updateShopDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result)
            {
                return NotFound();
            }
            return Ok("Название было заменено");
        }



        [HttpPost("GetInfo")]
        public async Task<ActionResult> Add([FromBody] GetShopsInfoDto shopDto)
        {
            bool result = false;
            try
            {
                ///result = await _shopService.CreateShop(shopDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result)
            {
                return NotFound();
            }
            return Ok("Магазин создан");
        }
    }
}
