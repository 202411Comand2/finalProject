using IdentityService.BLL.Abstractions.Utilities;

namespace IdentityService.BLL.Utilities
{
    public class BcryptDataHasher : IHasher
    {
        public string Hash(string value)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(value);
        }

        public bool Verify(string value, string hash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(value, hash);
        }
    }
}
