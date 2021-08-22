using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoQuotes.Infrastructure.Identity;
using Domain;
using Domain.Entities.AppUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoQuotes.Infrastructure
{
    public class DataSeed
    {
        public static async Task SeedDataAsync(DataContext context, IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityAppUser>>();
            var userRepository = services.GetRequiredService<IAppUserRepository>();
            var unitOfWorkFactory = services.GetRequiredService<IUnitOfWorkFactory>();
            
            if (!userManager.Users.Any())
            {
                var identityUser = new IdentityAppUser()
                {
                    UserName = "TestUserFirst",
                    Email = "testuserfirst@test.com",
                };

                await userManager.CreateAsync(identityUser, "qazwsX123@");

                using var unitOfWork = unitOfWorkFactory.Create();
                var user = new User(identityUser);

                await userRepository.Add(user);

                await unitOfWork.Apply();
            }
        }
    }
}