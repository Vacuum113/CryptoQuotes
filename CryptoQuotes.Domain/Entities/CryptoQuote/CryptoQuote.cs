using System;
using Domain.Abstractions;
using Domain.Abstractions.Entity;

namespace Domain.Entities.CryptoQuote
{
    // todo: mb create list of quotes like on CoinMarketCap "quotes" -> "usd, btc"
    public class CryptoQuote : IEntity<int>
    {
        internal CryptoQuote()
        {
            
        }
        
        public CryptoQuote(decimal price, float percentChangeOneHour, float percentChangeTwentyFourHours, decimal marketCap, DateTime lastUpdated)
        {
            Price = price;
            PercentChangeOneHour = percentChangeOneHour;
            PercentChangeTwentyFourHours = percentChangeTwentyFourHours;
            MarketCap = marketCap;
            LastUpdated = lastUpdated;
        }

        public int Id { get; private set;}
        public decimal Price { get; private set; }
        public float PercentChangeOneHour { get; private set; }
        public float PercentChangeTwentyFourHours { get; private set; }
        public decimal MarketCap { get; private set; }
        public DateTime LastUpdated { get; private set; }
        public bool IsActual { get; private set; }
        
        public Cryptocurrency.Cryptocurrency Cryptocurrency { get; private set; }
        public int CryptocurrencyId { get; private set; }

        public void SetCryptocurrency(Cryptocurrency.Cryptocurrency cryptocurrency)
        {
            if (cryptocurrency != null)
            {
                Cryptocurrency = cryptocurrency;
                CryptocurrencyId = cryptocurrency.Id;
            }
        }

        public void Deactivate() => IsActual = false;

        public void Activate() => IsActual = true;
    }
}