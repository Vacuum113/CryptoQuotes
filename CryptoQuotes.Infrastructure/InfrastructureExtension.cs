using Application.Identity;
using Application.UseCases.Queries.CryptocurrencyQuery;
using CryptoQuotes.Infrastructure.Identity;
using CryptoQuotes.Infrastructure.QueryHandlers;
using CryptoQuotes.Infrastructure.Repositories;
using Domain;
using Domain.Entities.AppUser;
using Domain.Entities.Cryptocurrency;
using Domain.Entities.CryptoQuote;
using Domain.Entities.IdentityAppUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoQuotes.Infrastructure
{
	public static class InfrastructureExtension
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionsString)
		{
			var builder = services.AddIdentityCore<IdentityAppUser>(o =>
			{
				o.Password.RequireLowercase = false;
				o.Password.RequiredLength = 6;
				o.Password.RequireUppercase = false;
				o.Password.RequireNonAlphanumeric = false;
				o.User.RequireUniqueEmail = true;
			});
			var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
			identityBuilder.AddEntityFrameworkStores<DataContext>();
			identityBuilder.AddSignInManager<SignInManager<IdentityAppUser >>();
			
			return services
				.AddDbContext<DataContext>(options => options
					.UseNpgsql(connectionsString,
						ob => ob.MigrationsAssembly(typeof(DataContext).Assembly.FullName)))
				.AddScoped<IUnitOfWorkFactory>(f => f.GetRequiredService<DataContext>())
				.AddScoped<ISignInManager, SignInManager>()
				.AddRepositories()
				.AddFactories();
		}
		
		
		private static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			return services
					.AddScoped<IIdentityAppUserRepository, IdentityAppUserRepository>()
					.AddScoped<IAppUserRepository, AppUserRepository>()
					.AddScoped<ICryptocurrencyRepository, CryptocurrencyRepository>()
					.AddScoped<ICryptoQuoteRepository, CryptoQuoteRepository>()
				;
		}
		
		private static IServiceCollection AddFactories(this IServiceCollection services)
		{
			return services
					.AddScoped<IIdentityAppUserFactory, EntitiesFactory>()
				;
		}
	}
}