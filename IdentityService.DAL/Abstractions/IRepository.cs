using IdentityService.Domain.Abstractions;

namespace IdentityService.DAL.Abstractions
{
    public interface IRepository<T> where T : class, IEntity
    {
        public IContextManager ContextManager { get; }
        public Task<T> Add(T entity);
        public Task<bool> Delete(int id);
        public Task<T?> Get(int id);
        public Task<IList<T>> GetAll();
        public Task<bool> Update(T entity);
    }
}
