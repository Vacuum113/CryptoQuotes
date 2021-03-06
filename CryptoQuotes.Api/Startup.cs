using Api.Extensions;
using Api.Middleware;
using Application;
using CryptoQuotes.Background.TaskRunners;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Provider;
using CryptoQuotes.Infrastructure;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
	        services
		        .AddApi(Configuration["TokenKey"])
		        .AddInfrastructure(Configuration.GetConnectionString("DatabaseConnection"))
		        .AddProvider(Configuration.GetSection("CoinMarketCapProvider"))
		        .AddTaskRunners()
		        .AddApplication()
		        .AddMediator();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseMiddleware<ErrorHandlingMiddleware>();

			app.UseDefaultFiles();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();
            

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});
        }
    }
}
