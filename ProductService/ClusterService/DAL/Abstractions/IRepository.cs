namespace DAL.Abstractions
{
	public interface IRepository<T> where T : class
	{
		public ApplicationDbContext CreateDatabaseContext();
		public Task<T> Get(int entityId);
		public Task<IList<T>> GetAll();
		public Task<T> Add(T entity);
		public Task<T> Update(T entity);
		public Task<T> SaveOrUpdate(T entity);
		public Task<bool> Delete(T entity);
	}
}
