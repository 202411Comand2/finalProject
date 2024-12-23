using DAL.Abstractions;
using Domain.Entities;

namespace DAL.Repositories
{
	public class ClusterRepository : BaseRepository<Cluster>
	{
		public ClusterRepository(IContextManager manager) : base(manager)
		{
		}
	}
}
