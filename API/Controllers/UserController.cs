using BLL.Identity.Abstractions;
using BLL.Identity.Dto;
using BLL.Identity.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [ApiController]
    [IgnoreAntiforgeryToken]
    [Route("[controller]")]
    public class UserController(IIdentityService identityService, IOptions<JwtOptions> jwtOptions) : ControllerBase
    {
        private readonly IIdentityService _identityService = identityService;
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

		[HttpPost("register")]
        public async Task<ActionResult<int>> Register([FromBody] RegisterUserDto registerModel)
        {
            User result = null;
            try
            {
				result = await _identityService.Register(registerModel);
			}
            catch (IdentityServiceException ex)
            {
                return BadRequest(ex.Message);
            }

            if (result == null) return NotFound();

            return Ok(result.Id);
        }
        [Authorize]
        [HttpPut("seller/register")]
        public async Task<IActionResult> RegisterSeller([FromBody] RegisterShopOwnerDto dto)
        {
            var isRegistered = await _identityService.RegisterSeller(dto);
            if (isRegistered) return Ok();
            else return BadRequest();
        }
        [Authorize]
        [HttpPost("pwreset")]
        public async Task<IActionResult> ChangePassword(ChangeUserPasswordDto dto)
        {
            var isChanged = await _identityService.ChangePassword(dto);
            if (isChanged) return Ok();
            else return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthDto dto)
        {
            var result = await _identityService.Login(dto);

            if (result == null) NotFound();

			Response.Cookies.Append(_jwtOptions.CookieName, result);
			return Ok();
        }
        [HttpPost("seller/login")]
        public async Task<IActionResult> SellerLogin([FromBody] AuthDto dto)
        {
            var token = Request.Cookies[_jwtOptions.CookieName];
            string result = string.Empty;
            if (token.IsNullOrEmpty())
            {
                result = await _identityService.BizLogin(dto);
            }
            else result = await _identityService.BizLogin(token);

            if (result == null) return NotFound();
            else
            {
                Response.Cookies.Append(_jwtOptions.CookieName, result);
                return Ok();
            }
        }

        [HttpGet("guest/login")]
        public async Task<ActionResult<string>> GetGuestToken()
        {
            var result = await _identityService.GetGuestToken();

            if (result == null) return NotFound();

			Response.Cookies.Append(_jwtOptions.CookieName, result);

			return new ActionResult<string>(result);
        }
        [HttpGet("info{userId}")]
        public async Task<ActionResult> GetInfo(int userId)
        {
            var info = await _identityService.GetUserInfo(userId);
            if (info == null) return NotFound();
            return new JsonResult(info);
        }
    }
}
