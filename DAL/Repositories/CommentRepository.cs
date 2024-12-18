using DAL.Abstractions;
using Domain.Entities;

namespace DAL.Repositories
{
	public class CommentRepository : BaseRepository<Comment>
	{
		public CommentRepository(IContextManager manager) : base(manager)
		{
		}
	}
}
