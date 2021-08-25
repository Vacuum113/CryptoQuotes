using System.Text;
using System.Text.Json;
using Api.Identity;
using Api.Services;
using Application;
using Application.Identity;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Api
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services, string tokenKey)
        {
            services.AddControllers(option =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                option.Filters.Add(new AuthorizeFilter(policy));
            })
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.IgnoreNullValues = true;
                    o.JsonSerializerOptions.IncludeFields = true;
                    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                    };
                }).AddIdentityCookies();
			
            return services
                .AddMediatR(typeof(ApplicationExtensions).Assembly)
                .AddHttpContextAccessor()
                .AddHttpClient()
                .AddSwaggerGen()
                .AddScoped<IIdentityService, IdentityService>()
                .AddScoped<IJwtGenerator, JwtGenerator>();
        }
    }
}