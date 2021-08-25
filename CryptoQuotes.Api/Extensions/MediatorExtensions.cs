using System;
using Application.Abstractions.Queries;
using Application.UseCases.Queries.CryptocurrencyQuery;
using CryptoQuotes.Infrastructure.QueryHandlers;
using FluentMediator;
using MediatR;
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

                    return await handler.Handle(request);
                });
            
            return builder;
        }
    }
}