using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoQuotes.Background.Entities;
using CryptoQuotes.Background.Entities.RepeatingTask;
using CryptoQuotes.Infrastructure.Identity;
using Domain;
using Domain.Abstractions;
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
            var repeatingTaskRepository = services.GetRequiredService<IRepeatingTaskRepository>();
            var dateTimeProvider = services.GetRequiredService<IDateTimeProvider>();

            using var unitOfWork = unitOfWorkFactory.Create();
            
            if (!userManager.Users.Any())
            {
                var identityUser = new IdentityAppUser()
                {
                    UserName = "TestUserFirst",
                    Email = "testuserfirst@test.com",
                };

                await userManager.CreateAsync(identityUser, "qazwsX123@");
                
                var user = new User(identityUser);

                await userRepository.Add(user);
            }

            var task = await repeatingTaskRepository.GetByTypeLatest(RepeatingTaskType.ImportCryptocurrency);
            if (task == null)
                await repeatingTaskRepository.Add(new RepeatingTask(RepeatingTaskType.ImportCryptocurrency, dateTimeProvider.Now));

            await unitOfWork.Apply();
        }
    }
}