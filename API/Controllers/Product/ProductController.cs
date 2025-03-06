using API.Models;
using BLL.Dto.Product;
using BLL.Identity;
using BLL.Identity.Abstractions;
using BLL.Identity.Exceptions;
using BLL.Products.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace API.Controllers.Product
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController(IProductService productService) : ControllerBase()
    {
        private readonly IProductService _productService = productService;

        /// <summary>
        /// Добавить продукт
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ActionResult<int>> Add([FromBody] AddProductDto product)
        {
            int result = -1;
            try
            {
                result = await _productService.AddProduct(product);
            }
            catch (Exception ex) // подсмотреть у Глеба
            {
                return BadRequest(ex.Message);
            }

            if (result==-1)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Обновить продукт
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<ActionResult<int>> Update([FromBody] UpdateProductDto product)
        {
            bool result = false;
            try
            {
                result = await _productService.UpdateProduct(product);
            }
            catch (Exception ex) // подсмотреть у Глеба
            {
                return BadRequest(ex.Message);
            }

            if (!result)
            {
                return NotFound();
            }
            return Ok(true);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<int>> Delete([FromBody] DeleteProductDto product)
        {
            bool result = false;
            try
            {
                result = await _productService.DeleteProduct(product);
            }
            catch (Exception ex) // подсмотреть у Глеба
            {
                return BadRequest(ex.Message);
            }

            if (!result)
            {
                return NotFound();
            }
            return Ok(true);
        }


        [HttpGet("GetShopProducts")]
        public async Task<ActionResult> GetShopProducts([FromQuery] GetallShopProductsDto product)
        {
            List<ProductDto> result = new List<ProductDto>();
            try
            {
                result = await _productService.GetShopProducts(product);
            }
            catch (Exception ex) // подсмотреть у Глеба
            {
                return BadRequest(ex.Message);
            }

            if (result is null)
            {
                return NotFound();
            }
            return new JsonResult(result);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            List<ProductDto> result = new List<ProductDto>();
            try
            {
                result = await _productService.GetAllProduct();
            }
            catch (Exception ex) // подсмотреть у Глеба
            {
                return BadRequest(ex.Message);
            }

            if (result is null)
            {
                return NotFound();
            }
            return new JsonResult(result);
        }

        [HttpGet("GetProductsByCluster")]
        public async Task<ActionResult> GetProductsByCluster([FromQuery] GetAllClusterProductsDto product)
        {
            List<ProductDto> result = new List<ProductDto>();
            try
            {
                result = await _productService.GetProductsByCluster(product);
            }
            catch (Exception ex) // подсмотреть у Глеба
            {
                return BadRequest(ex.Message);
            }

            if (result is null)
            {
                return NotFound();
            }
            return new JsonResult(result);
        }

    }
}
