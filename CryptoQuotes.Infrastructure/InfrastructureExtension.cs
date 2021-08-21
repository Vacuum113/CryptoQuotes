using Application.Identity;
using CryptoQuotes.Infrastructure.Identity;
using CryptoQuotes.Infrastructure.Repositories;
using Domain;
using Domain.Entities.AppUser;
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
			});
			var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
			identityBuilder.AddEntityFrameworkStores<DataContext>();
			identityBuilder.AddSignInManager<SignInManager<IdentityAppUser >>();
			
			return services
				.AddAutoMapper(typeof(InfrastructureExtensions).Assembly)
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