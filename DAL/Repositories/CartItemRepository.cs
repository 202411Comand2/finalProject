using DAL.Abstractions;
using Domain.Entities;

namespace DAL.Repositories
{
  
    public class CartItemRepository : BaseRepository<CartItem>
    {
        public CartItemRepository(IContextManager manager) : base(manager)
        {

        }
    }
}
