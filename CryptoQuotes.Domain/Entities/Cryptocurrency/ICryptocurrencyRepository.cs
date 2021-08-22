using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Abstractions.Repository;
using Domain.Entities.AppUser;

namespace Domain.Entities.Cryptocurrency
{
	public interface ICryptocurrencyRepository : IUpdateRepository<Cryptocurrency>, IGetOneByIdRepository<Cryptocurrency>, IFilterRepository<Cryptocurrency>, IAddRepository<Cryptocurrency>
	{
		Task<IEnumerable<Cryptocurrency>> All();
	}
}