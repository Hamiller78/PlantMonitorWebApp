using Microsoft.AspNetCore.SignalR;
using PlantMonitorWebApp.Server.Hubs;
using PlantMonitorWebApp.Shared.DataSources;
using PlantMonitorWebApp.Shared.Interfaces;
using PlantMonitorWebApp.Shared.MockClasses;
using PlantMonitorWebApp.Shared.TestClasses;

namespace PlantMonitorWebApp.Server.Services
{
    public class RestSensorService : IHostedService, IDisposable
    {
        private readonly ILogger<MockSensorService> _logger;
        private Timer _timer = null!;
        private IHubContext<SensorValueHub> _sensorHubContext;

        private Uri _uri1;
        private Uri _uri2;

        public RestSensorService(IConfiguration configuration,
                                 ILogger<MockSensorService> logger,
                                 IHubContext<SensorValueHub> sensorHubContext)
        {
            _logger = logger;
            _sensorHubContext = sensorHubContext;

            _uri1 = new Uri(configuration["Uri1"]);
            _uri2 = new Uri(configuration["Uri2"]);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SendValues, null, TimeSpan.Zero,
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

        private void SendValues(object? state)
        {
            IDataSource dataSource1 = new RestDataSource(_uri1);
            IDataSource dataSource2 = new RestDataSource(_uri2);


            _sensorHubContext.Clients.All.SendAsync("SensorValueChanged", "1", dataSource1.SensorValue);
            _sensorHubContext.Clients.All.SendAsync("SensorValueChanged", "2", dataSource2.SensorValue);
        }

    }
}
