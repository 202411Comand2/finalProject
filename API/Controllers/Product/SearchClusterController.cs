using BLL.Dto.Cluster;
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
        public async Task<ActionResult<int>> Add([FromBody] List<ISearchClusterService> searchClusterServices)
        {
            //int result = -1;
            //try
            //{
            //    result = await _clusterService.AddNewCluster(clusterDto);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}

            //if (result == -1)
            //{
            //    return NotFound();
            //}
            return Ok("result");
        }
    }
}
