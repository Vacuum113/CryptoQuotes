using System;
using Api.Identity;
using Application.Abstractions.Queries;
using Application.Abstractions.UseCases;
using Application.UseCases.Queries.CryptocurrencyQuery;
using Application.UseCases.UserIdentity.Login;
using Application.UseCases.UserIdentity.Registration;
using CryptoQuotes.Infrastructure.QueryHandlers;
using FluentMediator;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions
{
    public static class MediatorExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            return services.AddFluentMediator(b =>
            {
                b.AddQueryHandler<CryptocurrencyRequest, CryptocurrencyResponse, ICryptocurrencyQueryHandler, CryptocurrencyQueryHandler>(services);
                b.AddUseCaseCommand<LoginRequest, ILoginUseCase, LoginUseCase>(services);
                b.AddUseCaseCommand<RegistrationRequest, IRegistrationUseCase, RegistrationUseCase>(services);
            });
        }


        private static IPipelineProviderBuilder AddQueryHandler<TRequest, TResponse, TIQueryHandler, TQueryHandler>(this IPipelineProviderBuilder builder, IServiceCollection services)
            where TIQueryHandler : class, IGetManyQueryHandler<TRequest, TResponse>
            where TQueryHandler : class, IGetManyQueryHandler<TRequest, TResponse>, TIQueryHandler
        {
            services.AddScoped<TIQueryHandler, TQueryHandler>();
            builder.On<EntityRequest<TRequest>>().PipelineAsync()
                .Return<GetManyResponse<TResponse>, IServiceProvider>(async (provider, request) =>
                {
                    var handler = provider.GetRequiredService<TIQueryHandler>();
                    var authMiddleware = provider.GetRequiredService<AuthorizationMiddleware>();

                    if (!await authMiddleware.Process(handler, request))
                        return null; 
                    
                    return await handler.Handle(request);
                });
            
            return builder;
        }
        
        private static IPipelineProviderBuilder AddUseCaseCommand<TRequest, TIHandler, THandler>(
            this IPipelineProviderBuilder builder, IServiceCollection services)
            where TRequest : IRequest
            where TIHandler : class, IAsyncRequestHandler<TRequest>
            where THandler : class, TIHandler
        {
            services.AddScoped<TIHandler, THandler>();
            builder.On<TRequest>()
                .PipelineAsync()
                .Call<IServiceProvider>(async (provider, request) =>
                {
                    var useCase = provider.GetRequiredService<TIHandler>();
                    var authMiddleware = provider.GetRequiredService<AuthorizationMiddleware>();

                    if (!await authMiddleware.Process(useCase, request)) 
                        return;
                    
                    await useCase.Execute(request);
                });

            return builder;
        }
    }
}