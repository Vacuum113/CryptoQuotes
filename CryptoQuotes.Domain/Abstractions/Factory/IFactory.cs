using Domain.Abstractions.Entity;

namespace Domain.Abstractions.Factory
{
    public interface IFactory<out TEntity, in TFactoryEntity> 
        where TEntity: IEntity<int>
        where TFactoryEntity: IFactoryEntity
    {
        TEntity Create(TFactoryEntity entity);
    }
}