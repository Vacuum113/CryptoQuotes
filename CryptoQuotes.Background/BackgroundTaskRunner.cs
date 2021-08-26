using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CryptoQuotes.Background
{
    public sealed class BackgroundTaskRunner<TRunner> : BackgroundService
        where TRunner : IRepeatTaskRunner
    {
        private static readonly string TaskName = typeof(TRunner).Name;

        private readonly IServiceProvider _serviceProvider;

        public BackgroundTaskRunner(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();

                    var runner = scope.ServiceProvider.GetRequiredService<TRunner>();
                    var span = await runner.Run(stoppingToken);
                    
                    await Task.Delay(span, stoppingToken);
                }
                catch (TaskCanceledException e)
                {
                    if (stoppingToken.IsCancellationRequested)
                        return;
                }
                catch (Exception e)
                {
                    // todo: exception logging
                }
            }
        }
    }
}
