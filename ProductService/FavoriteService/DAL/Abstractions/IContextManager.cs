namespace FavoriteService.DAL
{
	public interface IContextManager
	{
		public ApplicationDbContext CreateDatabaseContext();
	}
}
