using ClusterService.BLL;
using ClusterService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Product
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchClusterController(ISearchClusterService searchClusterService, IClusterMainService clusterService//, IProductService productService)
                                                                                                                    ): ControllerBase
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
        public async Task<ActionResult<int>> Delete([FromBody] DeleteSearchClusterDto product)
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


        [HttpPut("Update")]
        public async Task<ActionResult<int>> Update([FromBody] UpdateSearchClusterDto product)
        {
            bool result = false;
            try
            {
                result = await _searchClusterService.UpdateSearchClusterService(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result)
            {
                return NotFound();
            }
            return Ok(true);
        }

        [HttpGet("GetKeyWordCluster")]
        public async Task<ActionResult> GetShopProducts([FromQuery] int id)
        {
            GetSearchClusterDto result = new GetSearchClusterDto();
            try
            {
                result = await _searchClusterService.GetSearchClusterId(id);
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


        [HttpGet("SearchProducts")]
        public async Task<ActionResult> SearchProducts([FromQuery]  string keyWord)
        {
            SearchClusterProductDto searchClusterProductDto = new SearchClusterProductDto();

            (List<Cluster>,List<int>) result ;
            try
            {
                result = await _searchClusterService.SearchProducts(keyWord);
               // var s = await _productService.GetProductsByCluster(result.Item2);
               // searchClusterProductDto.SearchClusterProductConnect(result.Item1,s);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }

            if (result.Item1 is null || result.Item2 is null)
            {
                return NotFound();
            }
            //List<ProductDto> productDtosList = new List<ProductDto>();
            //ProductDto productDto= new ProductDto();
            //productDto.Barcode = 1;
            //productDtosList.Add(productDto);
            //var s111 = new JsonResult(searchClusterProductDto);
            return new JsonResult(searchClusterProductDto);
        }

    }
}
