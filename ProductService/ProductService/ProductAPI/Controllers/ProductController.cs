using BLL.Dto;
using BLL.Dto.Product;
using BLL.Products.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Product
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProductService productService) : ControllerBase()
    {

       
            //[HttpGet]
            //public IActionResult GetAll() => Ok(new[] { "Laptop", "Phone" });

            [HttpGet("{id}")]
            public IActionResult GetById(int id) => Ok($"Product {id}");
       

        private readonly IProductService _productService = productService;

        /// <summary>
        /// Добавить продукт
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ActionResult<int>> Add([FromBody] AddProductDto product)
        {
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

        /// <summary>
        /// Обновить продукт
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<ActionResult<int>> Update([FromBody] UpdateProductDto product)
        {
            AnswerWithBackendDto<ProductDto> result = new();
            try
            {
                result = await _productService.UpdateProduct(product);
            }
            catch (Exception ex) // подсмотреть у Глеба
            {
                return BadRequest(ex.Message);
            }

            if (!result.DataReceived)
            {
                return BadRequest(result.ErrorLog);
            }
            return Ok(result.DataReceived);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<int>> Delete([FromBody] DeleteProductDto product)
        {
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

    }
}
