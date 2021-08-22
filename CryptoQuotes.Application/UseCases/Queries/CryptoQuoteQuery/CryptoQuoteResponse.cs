using System;

namespace Application.UseCases.Queries.CryptoQuoteQuery
{
    public record CryptoQuoteResponse
    {
        public int Id { get; init;}
        public decimal Price { get; init; }
        public float PercentChangeOneHour { get; init; }
        public float PercentChangeTwentyFourHours { get; init; }
        public decimal MarketCap { get; init; }
        public bool IsActual { get; init; }
        public DateTime LastUpdated { get; init;}
    }
}