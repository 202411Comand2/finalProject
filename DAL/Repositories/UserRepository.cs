using DAL.Abstractions;
using Domain.Entities;

namespace DAL.Repositories
{
	public class UserRepository : BaseRepository<User>
	{
		public UserRepository(IContextManager manager) : base(manager)
		{
		}
	}
}
