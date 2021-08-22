using CryptoQuotes.Background.TaskRunners.ImportCryptocurrencies;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoQuotes.Background.TaskRunners
{
    public static class TaskRunnersExtensions
    {
        public static IServiceCollection AddTaskRunners(this IServiceCollection services)
        {
            return services
                .AddScoped<ImportCryptocurrenciesTaskRunner>()
                .AddHostedService<BackgroundTaskRunner<ImportCryptocurrenciesTaskRunner>>();
        }
    }
}