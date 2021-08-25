using Application.Abstractions.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Presenters
{
    public static class PresentersExtensions
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            return services
                    .AddPresenter<ISimpleOutputPort, SimpleOutputPresenter>()
                ;
        }

        private static IServiceCollection AddPresenter<TPort, TPresenter>(this IServiceCollection services)
            where TPort : class
            where TPresenter : class, TPort
        {
            return services
                .AddScoped<TPresenter, TPresenter>()
                .AddScoped<TPort>(f => f.GetRequiredService<TPresenter>());
        }
    }
}