using DAL.Abstractions;
using Domain.Entities;

namespace DAL.Repositories
{
    
    public class OrderDetailsRepository : BaseRepository<OrderDetails>
    {
        public OrderDetailsRepository(IContextManager manager) : base(manager)
        {

        }
    }
}
