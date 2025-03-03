using Domain.Abstractions;

namespace BLL.Identity.Abstractions
{
    public interface IDto<TEntity, TDto>
    where TEntity : class, IDbEntity
    where TDto : IDto<TEntity, TDto>
    {
        TEntity ToEntity();
        TDto Parse(TEntity entity);
    }
}
