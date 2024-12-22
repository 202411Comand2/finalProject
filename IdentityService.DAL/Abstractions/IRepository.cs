namespace IdentityService.DAL.Abstractions
{
	public interface IRepository<T> where T : class
	{
		public Task<T> Get(int entityId);
		public Task<IList<T>> GetAll();
		public Task<T> Add(T entity);
		public Task<bool> Update(T entity);
		public Task<T> SaveOrUpdate(T entity);
		public Task<bool> Delete(int entityId);
	}
}
