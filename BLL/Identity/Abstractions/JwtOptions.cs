namespace BLL.Identity.Abstractions
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }
        public int ExpiresHours { get; set; }
        public string CookieName { get; set; }
        public string UsernameClaimName { get; set; }
        public string UserIdClaimName { get; set; }
        public string UserRoleClaimName { get; set; }
    }
}
