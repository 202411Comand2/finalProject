using DAL.Abstractions;
using FinalProjectEntityDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    
    public class FavoriteRepository : BaseRepository<Favorite>
    {
        public FavoriteRepository(IContextManager manager) : base(manager)
        {

        }
    }
}
