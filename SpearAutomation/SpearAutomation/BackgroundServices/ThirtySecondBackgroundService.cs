using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpearAutomation.Models.Logger.Data;
using SpearAutomation.Models.Spear.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpearAutomation.BackgroundServices
{
    public class ThirtySecondBackgroundService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;


        public ThirtySecondBackgroundService(IServiceProvider services, ILogger<ThirtySecondBackgroundService> logger)
        {
            _logger = logger;
            Services = services;
        }
        public IServiceProvider Services { get; }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background Service Starting");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<ISpearService>();

                scopedProcessingService.Update();
            }
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
