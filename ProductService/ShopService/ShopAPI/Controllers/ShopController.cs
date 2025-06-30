using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.DAL.Validatoin;
using ShopService.BLL;
using SupperBackEnd.Dto;

namespace API.Controllers.Product
{


    [ApiController]
    [Route("api/[controller]")]
    
    public class ShopController(IConfiguration configuration, IShopMainService shopService) : ControllerBase()
    {
        private readonly IShopMainService _shopService = shopService;
        private readonly IConfiguration _configuration = configuration;

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok($"Product {id}");

        /// <summary>
        /// Добавить магазин
        /// </summary>
        /// <param name="shopDto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("add")]
        public async Task<ActionResult<int>> Add([FromBody] AddShopDto shopDto)
        {
            Console.WriteLine("Зашел в сервис создания магазина");
            int idUser = new Validation(_configuration).ValidationToken(Request.Headers["Authorization"].FirstOrDefault());
            switch (idUser)
            {
                case -2: //токен просрочен
                    Console.WriteLine("Token is missing");
                    return Unauthorized("Token is missing");
                case -3:
                    Console.WriteLine("Invalid token claims");
                    return Unauthorized("Invalid token claims");
                default:
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
       [Authorize] 
        [HttpPut("Update")]
        public async Task<ActionResult<int>> UpdateName(UpdateShopDto updateShopDto)
        {
            Console.WriteLine("Зашел в сервис обновления магазина");
            int idUser = new Validation(_configuration).ValidationToken(Request.Headers["Authorization"].FirstOrDefault());
            switch (idUser)
            {
                case -2: //токен просрочен
                    Console.WriteLine("Token is missing");
                    return Unauthorized("Token is missing");
                case -3:
                    Console.WriteLine("Invalid token claims");
                    return Unauthorized("Invalid token claims");
                default:

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
        }

        [HttpPut("RestoreShop")]
        public async Task<ActionResult<int>> RestoreShop(RestoreShopDto restore) 
        {
            AnswerWithBackendDto<ShopDto> result = new();
            try
            {
                result = await _shopService.RestoreStore(restore);
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
        /// Получить список магазинов по указанным id
        /// </summary>
        /// <param name="shopDto"></param>
        /// <returns></returns>
        [HttpPost("GetInfoList")]
        public async Task<ActionResult<string>> GetInfoList([FromBody] GetShopsInfoDto shopDto)
        {
            AnswerWithBackendDto<ShopDto> result = new();
            try
            {
                result = await _shopService.GetShopsInfo(shopDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result.DataReceived)
            {
                return NotFound(result.ErrorLog);
            }
            return result.GetCollectionNotProblem();
        }


        /// <summary>
        /// Получить список магазинов по указанным id
        /// </summary>
        /// <param name="shopDto"></param>
        /// <returns></returns>
        [HttpGet("GetInfo")]
        public async Task<ActionResult<string>> GetInfo([FromQuery] int id)
        {
            AnswerWithBackendDto<ShopDto> result = new();
            try
            {
                result = await _shopService.GetShopsInfo(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result.DataReceived)
            {
                return NotFound(result.ErrorLog);
            }
            return Ok(result.ObjectDto);
        }
    }
}
