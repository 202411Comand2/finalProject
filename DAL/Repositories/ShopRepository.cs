using DAL.Abstractions;
using Domain.Entities;

namespace DAL.Repositories
{
	public class ShopRepository : BaseRepository<Shop>
	{
		public ShopRepository(IContextManager manager) : base(manager)
		{
		}
	}
}
