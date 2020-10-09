using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TvMaze.Interfaces;

namespace TvMaze.ScheduledServices
{
    internal class TimerService : IHostedService, IDisposable
    {
        private Timer _timer;
        private IServiceScopeFactory _scopeFactory;

        public TimerService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(async state => await(Execute(cancellationToken)), null, TimeSpan.Zero,TimeSpan.FromHours(24));
            return Task.CompletedTask;
        }

        private async Task Execute(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var tvMazeService = scope.ServiceProvider.GetService<ITVMazeService>();
                await tvMazeService.StartScrapingAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}