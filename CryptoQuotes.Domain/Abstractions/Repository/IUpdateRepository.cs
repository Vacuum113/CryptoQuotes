using System.Threading.Tasks;

namespace Domain.Abstractions.Repository
{
    public interface IUpdateRepository<in TEntity>
        where TEntity : IEntity<int>
    {
        Task Update(TEntity entity);
    }
}
