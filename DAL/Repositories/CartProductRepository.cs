using DAL.Abstractions;
using Domain.Entities;

namespace DAL.Repositories
{
	public class CartProductRepository : BaseRepository<CartProduct>
	{
		public CartProductRepository(IContextManager manager) : base(manager)
		{
		}
	}
}
