using DAL.Abstractions;
using Domain.Entities;

namespace DAL.Repositories
{
	public class ShopOwnerRepository : BaseRepository<ShopOwner>
	{
		public ShopOwnerRepository(IContextManager manager) : base(manager)
		{
		}
	}
}
