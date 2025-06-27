using Microsoft.AspNetCore.Mvc;

namespace OcelotWebApiGr.Controllers
{
    [ApiController]
    [Route("/")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Gateway is working!");
        }
    }
}
