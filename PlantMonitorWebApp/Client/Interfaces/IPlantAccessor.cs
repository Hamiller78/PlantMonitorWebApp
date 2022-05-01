using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Client.Interfaces
{
    public interface IPlantAccessor
    {
        Task<IEnumerable<Plant>> GetPlantsAsync();
        Task<Plant?> GetPlantByIdAsync(int id);
    }
}
