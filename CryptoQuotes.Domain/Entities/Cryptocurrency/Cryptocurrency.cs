using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions;

namespace Domain.Entities.Cryptocurrency
{
    public class Cryptocurrency : IEntity<int>
    {
        public Cryptocurrency(string name, string symbol, int coinMarketCapId)
        {
            Name = name;
            Symbol = symbol;
            CoinMarketCapId = coinMarketCapId;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int CoinMarketCapId { get; private set; }
        public string Symbol { get; private set; }
        
        public ICollection<CryptoQuote.CryptoQuote> CryptoQuote { get; private set; } = new List<CryptoQuote.CryptoQuote>();

        public void SetName(string name)
        {
            if (name != null)
                Name = name;
        }

        public void SetSymbol(string symbol)
        {
            if (symbol != null)
                Symbol = symbol;
        }

        public void SetCryptoQuote(IEnumerable<CryptoQuote.CryptoQuote> cryptoQuote)
        {
            CryptoQuote = cryptoQuote.ToList();
        }
    }
}