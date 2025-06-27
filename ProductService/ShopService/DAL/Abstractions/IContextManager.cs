namespace ShopService.DAL
{
	public interface IContextManager
	{
		public ApplicationDbContext CreateDatabaseContext();
	}
}
