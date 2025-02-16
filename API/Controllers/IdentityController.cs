using API.Models;
using BLL.Identity.Abstractions;
using BLL.Identity.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
	[Route("[controller]")]
	public class IdentityController : ControllerBase
	{
        private readonly IIdentityService _identityService;
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<int>> RegisterByPhone([FromBody] UserRegisterModel registerModel)
        {
            User result = null;
            try
            {
				result = await _identityService.Register(registerModel.Username,
				registerModel.Password,
				registerModel.Contact);
			}
            catch (IdentityServiceException ex)
            {
                return BadRequest(ex.Message);
            }

            if (result == null) return NotFound();

            return Ok();
        }
        [HttpGet("Login/{contact}")]
        public async Task<ActionResult<string>> Login(string contact, string password)
        {
            var result = await _identityService.Login(contact, password);

            if (result == null) return NotFound();

            return new ActionResult<string>(result);
        }
        [HttpGet("GetGuestToken")]
        public async Task<ActionResult<string>> GetGuestToken()
        {
            var result = await _identityService.GetGuestToken();

            if (result == null) return NotFound();

            return new ActionResult<string>(result);
        }
    }
}
