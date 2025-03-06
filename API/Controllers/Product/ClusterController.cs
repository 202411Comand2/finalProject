using API.Models;
using BLL.Dto.Cluster;
using BLL.Identity;
using BLL.Identity.Abstractions;
using BLL.Identity.Exceptions;
using BLL.Product;
using BLL.Products.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

//
namespace API.Controllers.Product
{
    [ApiController]
    [Route("[controller]")]
    public class ClusterController(IClusterService clusterService) : ControllerBase
    {
        private readonly IClusterService _clusterService = clusterService;

        [HttpPost("add")]
        public async Task<ActionResult<int>> Add([FromBody] AddClusterDto clusterDto)
        {
            int result = -1;
            try
            {
                result = await _clusterService.AddNewCluster(clusterDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (result==-1)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<int>> Update([FromBody] UpdateCluseterDto clusterDto)
        {
            bool result = false;
            try
            {
                result = await _clusterService.UpdateNameCluster(clusterDto);
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
        public async Task<ActionResult<int>> Delete(DeleteClusterDto deleteClusterDto)
        {
            bool result = false;
            try
            {
                result = await _clusterService.DeleteCluster(deleteClusterDto);
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


        /// <summary>
        /// Получить все элементы кластеров
        /// </summary>
        /// <returns>Коллекцию кластеров</returns>
        [HttpGet("GetAllElements")]
        public async Task<ActionResult> GetAllElements()
        {
            List<ClusterDto> result = new List<ClusterDto>();
            try
            {
                result = await _clusterService.GetAllElementsCluster();
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

        /// <summary>
        /// Получить только корневые элементы кластера
        /// </summary>
        [HttpGet("GetRootElements")]
        public async Task<ActionResult> GetRootElements()
        {
            List<ClusterDto> result = new List<ClusterDto>();
            try
            {
                result = await _clusterService.GetRootElementsCluster();
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


        ////TODO что делать ошибку кидать или возвращаться null
        /// <summary>
        /// Получить дочерние элементы кластера
        /// </summary>
        [HttpGet("GetChildrenElements")]
        public async Task<ActionResult> GetChildrenElements([FromQuery] GetClusterDto getClusterDto)
        {
            List<ClusterDto> result = new List<ClusterDto>();
            try
            {
                result = await _clusterService.GetChildrenElementsCluster(getClusterDto);
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
