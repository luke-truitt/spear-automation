using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpearAutomation.BackgroundServices
{
    public class FiveSecondBackgroundService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;

        public IServiceProvider Services { get; }
        public FiveSecondBackgroundService(IServiceProvider services, ILogger<FiveSecondBackgroundService> logger)
        {
            _logger = logger;
            Services = services;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background Service Starting");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Running Update");
            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background Service Stopping");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
