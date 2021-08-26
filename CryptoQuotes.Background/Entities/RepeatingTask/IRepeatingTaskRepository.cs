using System.Threading.Tasks;
using Domain.Abstractions.Repository;

namespace CryptoQuotes.Background.Entities.RepeatingTask
{
	public interface IRepeatingTaskRepository : IUpdateRepository<RepeatingTask>, IGetOneByIdRepository<RepeatingTask>, IFilterRepository<RepeatingTask>, IAddRepository<RepeatingTask>
	{
		Task<RepeatingTask> GetByTypeLatest(RepeatingTaskType repeatingTaskType);
	}
}