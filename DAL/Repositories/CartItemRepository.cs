using DAL.Abstractions;
using Domain.Entities;

namespace DAL.Repositories
{
    //Не развивается

    public class CartItemRepository : BaseRepository<CartItem>
    {
        public CartItemRepository(IContextManager manager) : base(manager)
        {

        }
    }
}
