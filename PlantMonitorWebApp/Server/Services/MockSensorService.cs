using PlantMonitorWebApp.Server.Hubs;
using PlantMonitorWebApp.Shared.Interfaces;
using PlantMonitorWebApp.Shared.MockClasses;
using PlantMonitorWebApp.Shared.TestClasses;

namespace PlantMonitorWebApp.Server.Services
{
    public class MockSensorService : IHostedService, IDisposable
    {
        private readonly ILogger<MockSensorService> _logger;
        private Timer _timer = null!;
        private SensorValueHub _sensorValueHub;

        public MockSensorService(ILogger<MockSensorService> logger, SensorValueHub sensorValueHub)
        {
            _logger = logger;
            _sensorValueHub = sensorValueHub;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SendValues, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

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

            _sensorValueHub.UpdateValue("1", dataSource1.SensorValue);
            _sensorValueHub.UpdateValue("2", dataSource2.SensorValue);
        }
    }
}
