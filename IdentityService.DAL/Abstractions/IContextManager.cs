namespace IdentityService.DAL.Abstractions
{
	/// <summary>
	/// DbContext factory interface
	/// </summary>
	public interface IContextManager
	{
		public ApplicationDbContext CreateDatabaseContext();
	}
}
