namespace CommentService.DAL
{
	public interface IContextManager
	{
		public ApplicationDbContext CreateDatabaseContext();
	}
}
