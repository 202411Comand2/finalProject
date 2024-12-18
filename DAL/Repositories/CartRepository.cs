using DAL.Abstractions;
using Domain.Entities;

namespace DAL.Repositories
{
	public class CartRepository : BaseRepository<Cart>
	{
		public CartRepository(IContextManager manager) : base(manager)
		{
		}
	}
}
