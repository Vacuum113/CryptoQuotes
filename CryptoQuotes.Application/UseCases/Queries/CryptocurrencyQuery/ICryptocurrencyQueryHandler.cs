using Application.UseCases.Queries.Abstractions;

namespace Application.UseCases.Queries.CryptocurrencyQuery
{
    public interface ICryptocurrencyQueryHandler : IGetManyQueryHandler<CryptocurrencyRequest, CryptocurrencyResponse>
    {
        
    }
}