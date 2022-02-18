using Microsoft.Extensions.Hosting;
using PlantMonitorWebApp.Server.Interfaces;

namespace PlantMonitorWebApp.Server.Services
{
    public class RestSensorService : BackgroundService
    {
        private readonly ILogger<RestSensorService> _logger;
        private readonly IDataUpdater _dataUpdater;

        public RestSensorService(IDataUpdater dataUpdater, ILogger<RestSensorService> logger)
        {
            _logger = logger;
            _dataUpdater = dataUpdater;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);

                try
                {
                    var workTask = _dataUpdater.RefreshData(null);
                    var cancelTask = Task.Delay(1000, stoppingToken);

                    //double await so exceptions from either task will bubble up
                    await await Task.WhenAny(workTask, cancelTask);

                    if (!workTask.IsCompletedSuccessfully)
                    {
                        _logger.LogError("Updater didn't complete successfully");
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError("Exception while updating sensor values.", ex.Message);
                }
            }
        }
    }
}
