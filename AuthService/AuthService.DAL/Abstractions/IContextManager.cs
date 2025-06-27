namespace AuthService.DAL
{
	public interface IContextManager
	{
		public ApplicationDbContext CreateDatabaseContext();
	}
}
