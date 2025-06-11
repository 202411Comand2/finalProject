using ClusterService.BLL;
using Microsoft.AspNetCore.Mvc;
using SupperBackEnd.Dto;

//
namespace API.Controllers.Product
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClusterController(IClusterMainService clusterService) : ControllerBase
    {
        private readonly IClusterMainService _clusterService = clusterService;

        [HttpPost("add")]
        public async Task<ActionResult<int>> Add([FromBody] AddClusterDto clusterDto)
        {
            AnswerWithBackendDto<ClusterDto> result = new();
            try
            {
                result = await _clusterService.AddNewCluster(clusterDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (!result.DataReceived)
            {
                return BadRequest(result.ErrorLog);
            }
            return Ok(result.ObjectDto.Id);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<int>> Update([FromBody] UpdateCluseterDto clusterDto)
        {
            AnswerWithBackendDto<ClusterDto> result = new();
            try
            {
                result = await _clusterService.UpdateNameCluster(clusterDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (!result.DataReceived)
            {
                return BadRequest(result.ErrorLog);
            }
            return Ok(result.ObjectDto);
        }


        [HttpDelete("Delete")]
        public async Task<ActionResult<bool>> Delete(DeleteClusterDto deleteClusterDto)
        {
            AnswerWithBackendDto<ClusterDto> result = new();
            try
            {
                result = await _clusterService.DeleteCluster(deleteClusterDto);
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


        /// <summary>
        /// Получить все элементы кластеров
        /// </summary>
        /// <returns>Коллекцию кластеров</returns>
        [HttpGet("GetAllElements")]
        public async Task<ActionResult> GetAllElements()
        {
            AnswerWithBackendDto<ClusterDto> result = new();
            try
            {
                result = await _clusterService.GetAllElementsCluster();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result.DataReceived)
            {
                return BadRequest(result.ErrorLog);
            }
            return Ok(result.GetCollectionWithProblem());
        }

        /// <summary>
        /// Получить только корневые элементы кластера
        /// </summary>
        [HttpGet("GetRootElements")]
        public async Task<ActionResult> GetRootElements()
        {
            AnswerWithBackendDto<ClusterDto> result = new();
            try
            {
                result = await _clusterService.GetRootElementsCluster();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result.DataReceived)
            {
                return BadRequest(result.ErrorLog);
            }
            return Ok(result.GetCollectionWithProblem());
        }


        ////TODO что делать ошибку кидать или возвращаться null
        /// <summary>
        /// Получить дочерние элементы кластера
        /// </summary>
        [HttpGet("GetChildrenElements")]
        public async Task<ActionResult> GetChildrenElements([FromQuery] GetClusterDto getClusterDto)
        {
            AnswerWithBackendDto<ClusterDto> result = new();
            try
            {
                result = await _clusterService.GetChildrenElementsCluster(getClusterDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result.DataReceived)
            {
                return BadRequest(result.ErrorLog);
            }
            return Ok(result.GetCollectionWithProblem());
        }
    }
}
