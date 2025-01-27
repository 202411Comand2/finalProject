using DAL.Abstractions;
using Domain.Entities;

namespace DAL.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(IContextManager manager) : base(manager)
        {
            
        }

		public override Task<User> Add(User entity)
		{
			return base.Add(entity);
		}
	}
}
