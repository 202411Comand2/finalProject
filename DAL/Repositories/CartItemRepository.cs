using DAL.Abstractions;
using FinalProjectEntityDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
  
    public class CartItemRepository : BaseRepository<CartItem>
    {
        public CartItemRepository(IContextManager manager) : base(manager)
        {

        }
    }
}
