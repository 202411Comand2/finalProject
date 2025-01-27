using DAL.Abstractions;
using FinalProjectEntityDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(IContextManager manager) : base(manager)
        {

        }
    }
}
