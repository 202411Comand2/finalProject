using DAL.Abstractions;
using Domain.Entities;

namespace DAL.Repositories
{
	public class RatingRepository : BaseRepository<Rating>
	{
		public RatingRepository(IContextManager manager) : base(manager)
		{
		}
	}
}
