using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Provider.Configuration;

namespace Provider
{
	public static class ProviderExtension
	{
		public static IServiceCollection AddProvider(this IServiceCollection services, IConfiguration providerTokenSection)
		{
			return services
				.Configure<CoinMarketCapProviderConfiguration>(providerTokenSection)
				.AddScoped<ICryptoQuotesClient, CryptoQuotesClient>()
				.AddScoped<ICryptoQuotesProvider, CryptoQuotesProvider>();
		}
	}
}