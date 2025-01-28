using DAL.Abstractions;
using Domain.Entities;


namespace DAL.Repositories
{
    public class AuthTokenRepository : BaseRepository<AuthToken>
    {
        public AuthTokenRepository(IContextManager manager) : base(manager)
        {

        }
    }
}
