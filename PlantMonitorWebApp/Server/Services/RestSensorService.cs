using Microsoft.AspNetCore.SignalR;
using PlantMonitorWebApp.Server.Hubs;
using PlantMonitorWebApp.Server.Interfaces;
using PlantMonitorWebApp.Shared.DataSources;
using PlantMonitorWebApp.Shared.Interfaces;
using PlantMonitorWebApp.Shared.MockClasses;
using PlantMonitorWebApp.Shared.TestClasses;

namespace PlantMonitorWebApp.Server.Services
{
    public class RestSensorService : IHostedService, IDisposable
    {
        private readonly ILogger<RestSensorService> _logger;
        private Timer _timer = null!;
        private IDataUpdater _dataUpdater;

        public RestSensorService(IDataUpdater dataUpdater, ILogger<RestSensorService> logger)
        {
            _logger = logger;
            _dataUpdater = dataUpdater;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(_dataUpdater.RefreshData, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
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
