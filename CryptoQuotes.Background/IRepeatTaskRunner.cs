using System;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoQuotes.Background
{
    public interface IRepeatTaskRunner
    {
        Task<TimeSpan> Run(CancellationToken stoppingToken);
    }
}