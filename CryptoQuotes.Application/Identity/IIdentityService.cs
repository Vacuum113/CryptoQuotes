using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.AppUser;
using Domain.Entities.IdentityAppUser;

namespace Application.Identity
{
    public interface IIdentityService
    {
        Task<IIdentityUser> GetCurrentUser();
    }
}
