using Domain.Abstractions.Repository;

namespace Domain.Entities.AppUser
{
	public interface IAppUserRepository : IUpdateRepository<User>, IGetOneByIdRepository<User>, IFilterRepository<User>, IAddRepository<User>
	{
	}
}