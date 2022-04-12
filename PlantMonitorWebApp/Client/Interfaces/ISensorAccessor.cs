using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Client.Interfaces
{
    public interface ISensorAccessor
    {
        Task<IEnumerable<Sensor>> GetSensorsAsync();
        Task<Sensor?> GetSensorByIdAsync(int id);
    }
}
