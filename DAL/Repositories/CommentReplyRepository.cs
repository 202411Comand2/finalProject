using DAL.Abstractions;
using Domain.Entities;

namespace DAL.Repositories
{
	public class CommentReplyRepository : BaseRepository<CommentReply>
	{
		public CommentReplyRepository(IContextManager manager) : base(manager)
		{
		}
	}
}
