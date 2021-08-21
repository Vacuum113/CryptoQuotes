using System.Threading.Tasks;
using Domain.Abstractions.Repository;

namespace Domain.Entities.IdentityAppUser
{
	public interface IIdentityAppUserRepository : IUpdateRepository<IIdentityUser>, IGetOneByIdRepository<IIdentityUser>, IFilterRepository<IIdentityUser>
	{
		Task<IIdentityUser> FindByIdentityId(int id);
		Task<IIdentityUser> FindByEmailAsync(string email);
	}
}