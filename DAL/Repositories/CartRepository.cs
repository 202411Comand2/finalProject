using DAL.Abstractions;
using FinalProjectEntityDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CartRepository : BaseRepository<Cart>
    {
        public CartRepository(IContextManager manager) : base(manager)
        {

        }
    }
}
