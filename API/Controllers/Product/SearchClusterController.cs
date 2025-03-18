using BLL.Dto.Cluster;
using BLL.Dto.Product;
using BLL.Dto.SearchCluster;
using BLL.Products;
using BLL.Products.Abstractions;
using BLL.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Product
{
    [ApiController]
    [Route("[controller]")]
    public class SearchClusterController(ISearchClusterService searchClusterService) : ControllerBase
    {

        private readonly ISearchClusterService _searchClusterService = searchClusterService;
      
        
        [HttpPost("add")]
        public async Task<ActionResult<int>> Add([FromBody] AddSearchClusterDto addSearchClusterDto)
        {
            bool result =false;
            try
            {
                result = await _searchClusterService.AddSearchClusterService(addSearchClusterDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }


        [HttpDelete("Delete")]
        public async Task<ActionResult<int>> Delete([FromBody] List<DeleteSearchClusterDto> product)
        {
            bool result = false;
            try
            {
                result = await _searchClusterService.DeleteSearchClusterService(product);
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

    }
}
