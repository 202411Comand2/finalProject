using Domain.Abstractions;

namespace Domain.Entities
{
    public class UserRegisterAttempt : ICacheEntity
    {
        public Guid Id { get; set; } = new Guid();
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int VerificationCode { get; set; }
    }
}
