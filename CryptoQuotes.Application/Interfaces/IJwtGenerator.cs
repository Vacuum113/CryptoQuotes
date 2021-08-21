using Domain;
using Domain.Entities;
using Domain.Entities.IdentityAppUser;

namespace Application.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(IIdentityUser user);
    }
}