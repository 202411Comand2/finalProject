namespace IdentityService.DAL.Abstractions
{
	public interface IContextManager
	{
		public IdentityDbContext CreateDatabaseContext();
	}
}
