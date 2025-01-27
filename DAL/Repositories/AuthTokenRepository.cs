using DAL.Abstractions;
using FinalProjectEntityDataBase.Entities;


namespace DAL.Repositories
{
    public class AuthTokenRepository : BaseRepository<AuthToken>
    {
        public AuthTokenRepository(IContextManager manager) : base(manager)
        {

        }
    }
}
