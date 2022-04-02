using PlantMonitorWebApp.Repository.Interfaces;
using PlantMonitorWebApp.Server.Interfaces;
using PlantMonitorWebApp.Shared.Factories;
using PlantMonitorWebApp.Shared.Interfaces;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Server.Services.DataUpdater
{
    public class RestDataUpdater : IDataUpdater
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageSender _messageSender;
        private readonly ILogger<RestDataUpdater> _logger;

        private Dictionary<string, IDataSource> _restDataSources = new();

        public RestDataUpdater(IServiceProvider serviceProvider,
                               IMessageSender messageSender,
                               ILogger<RestDataUpdater> logger)
        {
            _serviceProvider = serviceProvider;
            _messageSender = messageSender;
            _logger = logger;
            CreateDataSourcesFromDb();
        }

        public async Task RefreshData(object? state)
        {
            foreach (var dataSource in _restDataSources)
            {
                double newValue = dataSource.Value.SensorValue;
                await _messageSender.SendSensorValueChanged(dataSource.Key, newValue);
            }
        }

        private void CreateDataSourcesFromDb()
        {
            using var scope = _serviceProvider.CreateScope();
            var sensorRepository = scope.ServiceProvider.GetRequiredService<ISensorRepository>();
            var dataSourceFactory = scope.ServiceProvider.GetRequiredService<IDataSourceFactory>();

            IEnumerable<Sensor> sensorList = sensorRepository.GetAll();
            foreach (var sensor in sensorList)
            {
                try
                {
                    IDataSource dataSource = dataSourceFactory.CreateDataSource(sensor.ServiceUri);
                    _restDataSources.Add(sensor.Id.ToString().Trim(), dataSource);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error creating data source for Sensor {sensorid} with Uri {sensoruri}",
                                          sensor.Id,
                                          sensor?.ServiceUri);
                }

            }
        }
    }
}