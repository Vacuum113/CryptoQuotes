using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Provider;
using Provider.Configuration;

namespace CryptoQuotes.Background.TaskRunners.ImportCryptocurrencies
{
    public class ImportCryptocurrenciesTaskRunner : IRepeatTaskRunner
    {
        private readonly ICryptoQuotesProvider _provider;
        private static readonly TimeSpan TimeBetweenRuns = TimeSpan.FromMinutes(4);

        public ImportCryptocurrenciesTaskRunner(ICryptoQuotesProvider provider)
        {
            _provider = provider;
        }
        
        public async Task<TimeSpan> Run(CancellationToken stoppingToken)
        {
            var result = await _provider.GetLatest();
            // todo: add or update cryptocurrencies to db
        }
    }
}