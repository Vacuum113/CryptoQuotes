using Domain.Abstractions.Factory;

namespace Domain.Entities.IdentityAppUser
{
    public interface IIdentityAppUserFactory : IFactory<IIdentityUser, IdentityAppUserFactoryEntity>
    {
        
    }
}