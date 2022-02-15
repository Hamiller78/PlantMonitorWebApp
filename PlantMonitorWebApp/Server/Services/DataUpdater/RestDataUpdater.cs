using PlantMonitorWebApp.Server.Interfaces;
using PlantMonitorWebApp.Shared.DataSources;
using PlantMonitorWebApp.Shared.Interfaces;

namespace PlantMonitorWebApp.Server.Services.DataUpdater
{
    public class RestDataUpdater : IDataUpdater
    {
        private IDataSourceFactory _dataSourceFactory;
        private IConfiguration _config;
        private IMessageSender _messageSender;
        private ILogger<RestDataUpdater> _logger;

        private Dictionary<string, IDataSource> _restDataSources = new();

        public RestDataUpdater(IDataSourceFactory dataSourceFactory,
                               IConfiguration config,
                               IMessageSender messageSender,
                               ILogger<RestDataUpdater> logger)
        {
            _dataSourceFactory = dataSourceFactory;
            _config = config;
            _messageSender = messageSender;
            _logger = logger;
            CreateDataSourcesFromConfig();
        }

        public void RefreshData(object? state)
        {
            foreach (var dataSource in _restDataSources)
            {
                double newValue = dataSource.Value.SensorValue;
                _messageSender.SendSensorValueChanged(dataSource.Key, newValue);
            }
        }

        private void CreateDataSourcesFromConfig()
        {
            IEnumerable<IConfigurationSection> uriConfigEntries
                = _config
                    .GetSection("RestDataSources")
                    .GetChildren();
            foreach (var configEntry in uriConfigEntries)
            {
                if (!string.IsNullOrEmpty(configEntry.Value))
                {
                    var dataSource = _dataSourceFactory.CreateDataSource(new Uri(configEntry.Value));
                    _restDataSources.Add(configEntry.Key, dataSource);
                }
            }
        }
    }
}
