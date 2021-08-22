using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services
                    .AddAutoMapper(typeof(ApplicationExtensions).Assembly);
        }
    }
}