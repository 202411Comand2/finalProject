using BLL.Abstractions;
using IdentityService.Domain;

namespace BLL.Identity.Dto
{
    public class RegisterUserDto : IDto<User, RegisterUserDto>
    {
        public string Contact { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public RegisterUserDto Parse(User entity)
        {
            throw new NotImplementedException();
        }

        public User ToEntity()
        {
            throw new NotImplementedException();
        }
    }
}
