namespace ProductService.DAL
{
	public interface IContextManager
	{
		public ApplicationDbContext CreateDatabaseContext();
	}
}
