using System;
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
        
        public int CryptoQuoteId { get; init;}
        public decimal Price { get; init; }
        public float PercentChangeOneHour { get; init; }
        public float PercentChangeTwentyFourHours { get; init; }
        public decimal MarketCap { get; init; }
        public bool IsActual { get; init; }
        public DateTime LastUpdated { get; init;}
    }
}