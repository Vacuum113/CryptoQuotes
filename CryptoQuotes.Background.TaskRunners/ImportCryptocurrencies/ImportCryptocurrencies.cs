using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.Entities.Cryptocurrency;
using Domain.Entities.CryptoQuote;
using Microsoft.Extensions.Options;
using Provider;
using Provider.Configuration;
using Provider.Dtos;

namespace CryptoQuotes.Background.TaskRunners.ImportCryptocurrencies
{
    public class ImportCryptocurrenciesTaskRunner : IRepeatTaskRunner
    {
        private readonly ICryptoQuotesProvider _provider;
        private readonly ICryptocurrencyRepository _cryptocurrencyRepository;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly ICryptoQuoteRepository _cryptoQuoteRepository;
        private static readonly TimeSpan TimeBetweenRuns = TimeSpan.FromHours(1);

        public ImportCryptocurrenciesTaskRunner(ICryptoQuotesProvider provider, ICryptocurrencyRepository cryptocurrencyRepository, 
            IUnitOfWorkFactory unitOfWorkFactory, ICryptoQuoteRepository cryptoQuoteRepository)
        {
            _provider = provider;
            _cryptocurrencyRepository = cryptocurrencyRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _cryptoQuoteRepository = cryptoQuoteRepository;
        }

        public async Task<TimeSpan> Run(CancellationToken stoppingToken)
        {
            // todo refactoring
            var result = await _provider.GetLatest();
            if (result.Status.ErrorCode != ResponseStatusCode.Success)
                return TimeBetweenRuns;

            var cryptocurrencies = new List<Cryptocurrency>();
            foreach (var cc in result.Data)
                cryptocurrencies.Add(CreateNewCC(cc));

            var ccs = (await _cryptocurrencyRepository.All()).ToList();
            var updatedCc = new List<Cryptocurrency>();
            var addedCq = new List<CryptoQuote>();
            var addedCc = new List<Cryptocurrency>();
            
            using var unitOfWork = _unitOfWorkFactory.Create();
            
            foreach (var cc in cryptocurrencies)
            {
                var existCc = ccs.FirstOrDefault(c => c.CoinMarketCapId == cc.CoinMarketCapId);
                if (existCc != null)
                {
                    if (TryUpdateCryptocurrency(existCc, cc))
                        updatedCc.Add(existCc);

                    var actualCq = existCc.CryptoQuote.First(cq => cq.IsActual == true);
                    if (actualCq.LastUpdated == cc.CryptoQuote.First().LastUpdated)
                        continue;
                    
                    actualCq.Deactivate();
                    
                    var cryptoQuote = cc.CryptoQuote.First();
                    cryptoQuote.SetCryptocurrency(existCc);
                    cryptoQuote.Activate();

                    addedCq.Add(cryptoQuote);
                }
                else
                {
                    cc.CryptoQuote.First().Activate();
                    addedCc.Add(cc);
                }

            }

            await _cryptocurrencyRepository.UpdateRange(updatedCc);
            await _cryptocurrencyRepository.AddRange(addedCc);
            await _cryptoQuoteRepository.AddRange(addedCq);

            await unitOfWork.Apply();
            
            return TimeBetweenRuns;
        }

        private bool TryUpdateCryptocurrency(Cryptocurrency existCc, Cryptocurrency cc)
        {
            var changed = false;
            if (existCc.Name != cc.Name)
            {
                existCc.SetName(cc.Name);
                changed = true;
            }

            if (existCc.Symbol != cc.Symbol)
            {
                existCc.SetSymbol(cc.Symbol);
                changed = true;
            }

            return changed;
        }

        private Cryptocurrency CreateNewCC(CryptocurrencyData data)
        {
            var cc = new Cryptocurrency(data.Name, data.Symbol, data.Id);
            var usd = data.Quote.Usd;
            cc.CryptoQuote.Add(new CryptoQuote(usd.Price, usd.PercentChange_1h, usd.PercentChange_24h, usd.MarketCap, usd.LastUpdated));
            return cc;
        }
    }
}