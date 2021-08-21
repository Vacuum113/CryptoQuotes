using System.Text;
using API.Identity;
using API.Services;
using Application.Identity;
using Application.Interfaces;
using Application.UseCases.UserIdentity.Login;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services, string tokenKey)
        {
            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                option.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Latest);

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
                .AddMediatR(typeof(LoginHandler).Assembly)
                .AddHttpContextAccessor()
                .AddHttpClient()
                .AddSwaggerGen()
                .AddScoped<IIdentityService, IdentityService>()
                .AddScoped<IJwtGenerator, JwtGenerator>();
        }
    }
}