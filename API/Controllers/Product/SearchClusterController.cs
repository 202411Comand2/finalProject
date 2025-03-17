using BLL.Dto.Cluster;
using BLL.Dto.SearchCluster;
using BLL.Products;
using BLL.Products.Abstractions;
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
    }
}
