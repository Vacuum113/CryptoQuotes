using System.Collections.Generic;
using Application.UseCases.Queries.CryptoQuoteQuery;
using Domain.Entities.CryptoQuote;

namespace Application.UseCases.Queries.CryptocurrencyQuery
{
    public record CryptocurrencyResponse
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int CoinMarketCapId { get; init; }
        public string Symbol { get; init; }
        
        public IEnumerable<CryptoQuoteResponse> CryptoQuote { get; set; }

    }
}