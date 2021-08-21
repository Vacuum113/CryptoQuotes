using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Abstractions.Repository
{
    public interface IGetManyByIdsRepository<TEntity>
        where TEntity : IEntity<int>
    {
        Task<IList<TEntity>> Get(IList<int> ids);
    }
}
