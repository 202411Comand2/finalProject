using DAL.Abstractions;
using FinalProjectEntityDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    
    public class OrderDetailsRepository : BaseRepository<OrderDetails>
    {
        public OrderDetailsRepository(IContextManager manager) : base(manager)
        {

        }
    }
}
