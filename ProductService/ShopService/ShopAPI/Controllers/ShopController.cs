using BLL.Dto;
using BLL.Dto.Shop;
using BLL.Products.Abstractions;
using Microsoft.AspNetCore.Authorization;
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

            AnswerWithBackendDto<ShopDto> result = new();
            try
            {
               result = await _shopService.CreateShop(shopDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result.DataReceived)
            {
                return BadRequest(result.ErrorLog);
            }
            //В этом случае, если объект успешно создан, клиент получит статус 200 OK и JSON с данными объекта
            return Ok(result.ObjectDto.Id);
        }

        //TODO что делать с товарами при удалении магазина, нужно как-то пробегаться
        [HttpDelete("Delete")]
        public async Task<ActionResult<int>> Delete(DeleteShopDto shopDto)
        {
            AnswerWithBackendDto<ShopDto> result = new();
          
            try
            {
                result = await _shopService.DeleteShop(shopDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (!result.DataReceived)
            {
                return BadRequest(result.ErrorLog);
            }
            return Ok(true);
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
            AnswerWithBackendDto<ShopDto> result = new();
            try
            {
                result = await _shopService.UpdateNameShop(updateShopDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result.DataReceived)
            {
                return BadRequest(result.ErrorLog);
            }
            return Ok(true);
        }

        [HttpPost("GetInfo")]
        public async Task<ActionResult> GetInfo([FromBody] GetShopsInfoDto shopDto)
        {
            List<ShopDto> result = new List<ShopDto>();
            try
            {
                result = await _shopService.GetShopsInfo(shopDto);
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
