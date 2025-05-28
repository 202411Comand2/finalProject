using IdentityService.Domain.Abstractions;

namespace BLL.Abstractions
{
    public interface IDto<TEntity, TDto>
    where TEntity : class, IEntity
    where TDto : IDto<TEntity, TDto>
    {
        TEntity ToEntity();
        TDto Parse(TEntity entity);
    }
}
