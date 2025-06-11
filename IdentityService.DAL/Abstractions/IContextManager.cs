namespace IdentityService.DAL
{
	public interface IContextManager
	{
		public IdentityDbContext CreateDatabaseContext();
	}
}
