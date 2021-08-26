using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Abstractions.Entity;

namespace Domain.Abstractions.Repository
{
    public interface IAddRepository<in TEntity>
        where TEntity : IEntity<int>
    {
        Task Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> addedCc);
    }
}
