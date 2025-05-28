using IdentityService.Domain.Abstractions;

namespace IdentityService.DAL.Abstractions
{
    public interface IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Generates DbContext class instances
        /// </summary>
        public IContextManager ContextManager { get; }
        /// <summary>
        /// Use to add single entity
        /// </summary>
        /// <param name="entity">Instance of IEntity class</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Entity with updated id property</returns>
        public Task<T> Add(T entity, CancellationToken? cancellationToken = null);
        public Task<bool> Delete(int id, CancellationToken? cancellationToken = null);
        public Task<T?> Get(int id, CancellationToken? cancellationToken = null);
        public Task<IList<T>> GetAll(CancellationToken? cancellationToken = null);
        public Task<bool> Update(T entity, CancellationToken? cancellationToken = null);
    }
}
