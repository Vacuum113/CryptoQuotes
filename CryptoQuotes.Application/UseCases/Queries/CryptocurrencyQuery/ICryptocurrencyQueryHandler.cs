using Application.Abstractions.Queries;

namespace Application.UseCases.Queries.CryptocurrencyQuery
{
    public interface ICryptocurrencyQueryHandler : IGetManyQueryHandler<CryptocurrencyRequest, CryptocurrencyResponse>
    {
        
    }
}