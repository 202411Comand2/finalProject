using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.DAL.Validatoin;
using ProductService.BLL;
using SupperBackEnd.Dto;
using System.Web.Http;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace API.Controllers.Product
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IConfiguration configuration, IProductMainService productService) : ControllerBase()
    {
        private readonly IProductMainService _productService = productService;
        private readonly IConfiguration _configuration = configuration;


        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok($"Product {id}");
       

      
        /// <summary>
        /// Добавить продукт
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddProductDto product)
        {
            Console.WriteLine("Зашел в сервис добавление товара");
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
                    AnswerWithBackendDto<ProductDto> result = new();
                    try
                    {
                        result = await _productService.AddProduct(product);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }

                    if (!result.DataReceived)
                    {
                        return BadRequest(result.ErrorLog);
                    }
                    return Ok(result.ObjectDto.ProductId);
            }
        }

        /// <summary>
        /// Обновить продукт
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("Update")]
        public async Task<ActionResult<int>> Update([FromBody] UpdateProductDto product)
        {
            Console.WriteLine("Зашел в сервис обновление товара");
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
                    AnswerWithBackendDto<ProductDto> result = new();
                    try
                    {
                        result = await _productService.UpdateProduct(product);
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
       
        [Authorize]
        [HttpDelete("Delete")]
        public async Task<ActionResult<int>> Delete([FromBody] DeleteProductDto product)
        {
            Console.WriteLine("Зашел в сервис удаления товара");
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
                    AnswerWithBackendDto<ProductDto> result = new();
                    try
                    {
                        result = await _productService.DeleteProduct(product);
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


        [HttpGet("GetShopProducts")]
        public async Task<ActionResult<string>> GetShopProducts([FromQuery] GetallShopProductsDto product)
        {
            AnswerWithBackendDto<ProductDto> result = new();
            try
            {
                result = await _productService.GetShopProducts(product);
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

        [HttpGet("GetAll")]
        public async Task<ActionResult<string>> GetAll()
        {
            AnswerWithBackendDto<ProductDto> result = new();
            try
            {
                result = await _productService.GetAllProduct();
            }
            catch (Exception ex) // подсмотреть у Глеба
            {
                return BadRequest(ex.Message);
            }
            if (!result.DataReceived)
            {
                return BadRequest(result.ErrorLog);
            }
            return result.GetCollectionNotProblem();
        }

        [HttpGet("GetProductsByCluster")]
        public async Task<ActionResult<string>> GetProductsByCluster([FromQuery] GetAllClusterProductsDto product)
        {
            AnswerWithBackendDto<ProductDto> result = new();
            try
            {
                result = await _productService.GetProductsByCluster(product);
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

        [HttpGet("GetProductsById")]
        public async Task<ActionResult<string>> GetProductId([FromQuery] int id )
        {
            try
            {
                var result = await _productService.GetProductById(id);

                if (!result.DataReceived)
                {
                    return Ok(new ApiResponse<ProductDto>(false, null, result.ErrorLog));
                }

                return Ok(new ApiResponse<ProductDto>(true, result.ObjectDto, null));
                
            }
            catch (Exception ex)
            {
                return Ok(new ApiResponse<ProductDto>(false, null, ex.Message));
            }
        }

        [HttpGet("GetProductsByIds")]
        public async Task<ActionResult<string>> GetProductsByIds([FromUri] int productIds)
        {
            AnswerWithBackendDto<ProductDto> result = new();
            try
            {
                result = await _productService.GetProductByListId(productIds);
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
