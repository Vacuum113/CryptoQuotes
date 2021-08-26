using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CryptoQuotes.Background.Entities;
using CryptoQuotes.Background.Entities.RepeatingTask;
using Domain;
using Domain.Abstractions;
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
        private readonly IRepeatingTaskRepository _repeatingTaskRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private static readonly TimeSpan TimeBetweenRuns = TimeSpan.FromHours(1);

        public ImportCryptocurrenciesTaskRunner(ICryptoQuotesProvider provider, ICryptocurrencyRepository cryptocurrencyRepository, 
            IUnitOfWorkFactory unitOfWorkFactory, ICryptoQuoteRepository cryptoQuoteRepository, 
            IRepeatingTaskRepository repeatingTaskRepository, IDateTimeProvider dateTimeProvider)
        {
            _provider = provider;
            _cryptocurrencyRepository = cryptocurrencyRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
            _cryptoQuoteRepository = cryptoQuoteRepository;
            _repeatingTaskRepository = repeatingTaskRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<TimeSpan> Run(CancellationToken stoppingToken)
        {
            var (canExecute, task) = await CanExecuteTask();
            if (!canExecute)
                return TimeBetweenRuns;
            
            var result = await _provider.GetLatest();
            if (result.Status.ErrorCode != ResponseStatusCode.Success)
                return TimeBetweenRuns;

            using var unitOfWork = _unitOfWorkFactory.Create();

            await ProcessingCryptocurrencyResponse(result);

            UpdateTask(task);
            
            await unitOfWork.Apply();

            return TimeBetweenRuns;
        }

        private void UpdateTask(RepeatingTask task)
        {
            task.SetExecuteDate(_dateTimeProvider.Now.AddHours(1));
        }

        private async Task<(bool canExecute, RepeatingTask task)> CanExecuteTask()
        {
            var now = _dateTimeProvider.Now;
            var task = await _repeatingTaskRepository.GetByTypeLatest(RepeatingTaskType.ImportCryptocurrency);
            return (task != null && task.ExecuteDate <= now, task);
        }

        private async Task ProcessingCryptocurrencyResponse(СryptocurrencyResponse result)
        {
            var cryptocurrencies = CreateCcFromCcResponse(result);

            var currentCcs = (await _cryptocurrencyRepository.All()).ToList();
            var updatedCc = new List<Cryptocurrency>();
            var addedCq = new List<CryptoQuote>();
            var addedCc = new List<Cryptocurrency>();
            
            foreach (var newCryptocurrency in cryptocurrencies)
            {
                var existCc = currentCcs.FirstOrDefault(c => c.CoinMarketCapId == newCryptocurrency.CoinMarketCapId);
                if (existCc != null)
                    UpdateCc(existCc, newCryptocurrency, updatedCc, addedCq);
                else
                    AddCc(newCryptocurrency, addedCc);
            }

            await _cryptocurrencyRepository.UpdateRange(updatedCc);
            await _cryptocurrencyRepository.AddRange(addedCc);
            await _cryptoQuoteRepository.AddRange(addedCq);
        }

        private void AddCc(Cryptocurrency newCryptocurrency, List<Cryptocurrency> addedCc)
        {
            newCryptocurrency.CryptoQuote.First().Activate();
            addedCc.Add(newCryptocurrency);
        }

        private void UpdateCc(Cryptocurrency existCc, Cryptocurrency newCryptocurrency, List<Cryptocurrency> updatedCc, List<CryptoQuote> addedCq)
        {
            if (TryUpdateCryptocurrency(existCc, newCryptocurrency))
                updatedCc.Add(existCc);

            var actualCq = existCc.CryptoQuote.First(cq => cq.IsActual == true);
            if (actualCq.LastUpdated == newCryptocurrency.CryptoQuote.First().LastUpdated)
                return;
                    
            actualCq.Deactivate();
                    
            var cryptoQuote = newCryptocurrency.CryptoQuote.First();
            cryptoQuote.SetCryptocurrency(existCc);
            cryptoQuote.Activate();

            addedCq.Add(cryptoQuote);
        }

        private List<Cryptocurrency> CreateCcFromCcResponse(СryptocurrencyResponse result)
        {
            return result.Data.Select(cc => CreateNewCC(cc)).ToList();
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