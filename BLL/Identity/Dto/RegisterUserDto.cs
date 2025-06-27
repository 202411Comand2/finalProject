using BLL.Identity.Abstractions;
using BLL.Identity.Extensions;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Identity.Dto
{
    public class RegisterUserDto
    {
        public string Contact { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
