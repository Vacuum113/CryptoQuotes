using API.Middleware;
using Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Provider;
using CryptoQuotes.Infrastructure;

namespace API
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
		        .AddApplication();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseAuthentication();
			app.UseMvcWithDefaultRoute();
			
			app.UseForwardedHeaders(new ForwardedHeadersOptions {
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});
			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});
        }
    }
}
