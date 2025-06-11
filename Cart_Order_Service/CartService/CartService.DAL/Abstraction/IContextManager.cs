namespace CartService.DAL.Abstractions
{
	public interface IContextManager
	{
		public ApplicationDbContext CreateDatabaseContext();
	}
}
