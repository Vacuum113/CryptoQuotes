using System.Threading.Tasks;
using Domain.Abstractions.Entity;

namespace Domain.Abstractions.Repository
{
    public interface IGetOneByIdRepository<TEntity>
        where TEntity : IEntity<int>
    {
        Task<TEntity> Get(int id);
    }
}
