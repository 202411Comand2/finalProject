using DAL.Abstractions;
using Domain.Entities;

namespace DAL.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(IContextManager manager) : base(manager)
        {

        }
    }
}
