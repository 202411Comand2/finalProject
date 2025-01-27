using DAL.Abstractions;
using FinalProjectEntityDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RatingRepository : BaseRepository<Rating>
    {
        public RatingRepository(IContextManager manager) : base(manager)
        {

        }
    }
}
