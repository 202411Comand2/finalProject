using DAL.Abstractions;
using Domain.Entities;

namespace DAL.Repositories
{
	public class ProductRepository : BaseRepository<Product>
	{
		public ProductRepository(IContextManager manager) : base(manager)
		{
		}
	}
}
