using DAL.Abstractions;
using Domain.Entities;


namespace DAL.Repositories
{
    public class AccessTokenRepository : BaseRepository<AccessToken>
    {
        public AccessTokenRepository(IContextManager manager) : base(manager)
        {

        }
    }
}
