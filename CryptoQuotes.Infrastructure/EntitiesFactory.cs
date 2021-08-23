using CryptoQuotes.Infrastructure.Identity;
using Domain.Abstractions.Factory;
using Domain.Entities.IdentityAppUser;
using Microsoft.AspNetCore.Identity;

namespace CryptoQuotes.Infrastructure
{
    internal class EntitiesFactory : IIdentityAppUserFactory
    {
        private readonly UserManager<IdentityAppUser> _userManager;

        public EntitiesFactory(UserManager<IdentityAppUser> userManager)
        {
            _userManager = userManager;
        }

        public IIdentityUser Create(IdentityAppUserFactoryEntity entity)
        {
            var user = new IdentityAppUser()
            {
                Email = entity.Email,
                UserName = entity.Email
            };
            
            var result = _userManager.CreateAsync(user, entity.Password).Result;

            return !result.Succeeded ? null : user;
        }
    }
}