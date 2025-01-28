using DAL.Abstractions;
using Domain.Entities;

namespace DAL.Repositories
{
    
    public class FavoriteRepository : BaseRepository<Favorite>
    {
        public FavoriteRepository(IContextManager manager) : base(manager)
        {

        }
    }
}
