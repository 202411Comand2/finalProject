using API.Models;
using BLL.Dto;
using BLL.Identity;
using BLL.Identity.Abstractions;
using BLL.Identity.Exceptions;
using BLL.Products.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{



    [ApiController]
    [Route("[controller]")]
    public class ProductController(IProductService productService) : ControllerBase()
    {
        private readonly IProductService _productService;

        [HttpPost("add")]
        public async Task<ActionResult<int>> add([FromBody]  ProductDto product)
        {
            bool result =false;
            try
            {
                result = await _productService.AddProduct( product);
            }
            catch (Exception ex) // подсмотреть у Глеба
            {
                return BadRequest(ex.Message);
            }

            if (result == null) return NotFound();

            return Ok();
        }
    }
}
