﻿namespace PlantMonitorWebApp.Server.Services
{
    public class RestSensorService : IHostedService, IDisposable
    {
        private readonly ILogger<MockSensorService> _logger;
        private Timer _timer = null!;
        private IHubContext<SensorValueHub> _sensorHubContext;

        public MockSensorService(ILogger<MockSensorService> logger, IHubContext<SensorValueHub> sensorHubContext)
        {
            _logger = logger;
            _sensorHubContext = sensorHubContext;
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
            ISeedSource seedSource = new SecondsOfDaySeedSource();
            IDataSource dataSource1 = new MockDailyData(seedSource);
            IDataSource dataSource2 = new MockSinePerMinuteData(seedSource);

            _sensorHubContext.Clients.All.SendAsync("SensorValueChanged", "1", dataSource1.SensorValue);
            _sensorHubContext.Clients.All.SendAsync("SensorValueChanged", "2", dataSource2.SensorValue);
        }

    }
}
