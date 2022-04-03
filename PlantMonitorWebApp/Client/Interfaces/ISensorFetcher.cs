using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Client.Interfaces
{
    public interface ISensorFetcher
    {
        Task<IEnumerable<Sensor>> FetchSensorsAsync();
        Task<Sensor?> GetSensorByIdAsync(int id);
    }
}
