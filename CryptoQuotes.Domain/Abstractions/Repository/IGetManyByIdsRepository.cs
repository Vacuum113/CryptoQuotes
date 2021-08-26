using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Abstractions.Entity;

namespace Domain.Abstractions.Repository
{
    public interface IGetManyByIdsRepository<TEntity>
        where TEntity : IEntity<int>
    {
        Task<IList<TEntity>> Get(IList<int> ids);
    }
}
