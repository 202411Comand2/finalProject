using API.Models;
using BLL.Identity.Abstractions;
using BLL.Identity.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [ApiController]
	[Route("[controller]")]
	public class UserController(IIdentityService identityService, IOptions<JwtOptions> jwtOptions) : ControllerBase
	{
        private readonly IIdentityService _identityService = identityService;
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

		[HttpPost("register")]
        public async Task<ActionResult<int>> Register([FromBody] UserModel registerModel)
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
        [Authorize]
        [HttpPut("{shopId}/addUser")]
        public async Task<IActionResult> RegisterManager(int shopId, int userId)
        {
            throw new NotImplementedException();
            return Ok();
        }
        [Authorize]
        [HttpPost("{shopId}/regUser")]
        public async Task<ActionResult<string>> RegisterSeller(int shopId)
        {
            var isRegistered = await _identityService.RegisterSeller(Request.Cookies[_jwtOptions.CookieName], shopId);
            if (isRegistered) return Ok();
            else return NotFound();
        }
        [HttpPost("seller/login")]
        public async Task<IActionResult> SellerLogin([FromBody] UserModel model)
        {
            var token = Request.Cookies[_jwtOptions.CookieName];
            string result = string.Empty;
            if (token.IsNullOrEmpty())
            {
                result = await _identityService.BizLogin(model.Contact, model.Password);
            }
            else result = await _identityService.BizLogin(token);

            if (result == null) return NotFound();
            else
            {
                Response.Cookies.Append(_jwtOptions.CookieName, result);
                return Ok();
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserModel model)
        {
            var result = await _identityService.Login(model.Contact, model.Password);

            if (result == null) NotFound();

			Response.Cookies.Append(_jwtOptions.CookieName, result);
			return Ok();
        }

        [HttpGet("guestLogin")]
        public async Task<ActionResult<string>> GetGuestToken()
        {
            var result = await _identityService.GetGuestToken();

            if (result == null) return NotFound();

			Response.Cookies.Append(_jwtOptions.CookieName, result);

			return new ActionResult<string>(result);
        }
    }
}
