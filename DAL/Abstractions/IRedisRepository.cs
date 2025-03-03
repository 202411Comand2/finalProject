using Domain.Abstractions;

namespace DAL.Abstractions
{
    public interface IRedisRepository<T> where T : ICacheEntity
    {
        public Task Add(T entity);
        public Task<T?> Get(Guid id);
    }
}
