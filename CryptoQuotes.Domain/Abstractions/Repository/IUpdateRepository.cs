using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Abstractions.Entity;

namespace Domain.Abstractions.Repository
{
    public interface IUpdateRepository<in TEntity>
        where TEntity : IEntity<int>
    {
        Task Update(TEntity entity);
        Task UpdateRange(IEnumerable<TEntity> entities);
    }
}
