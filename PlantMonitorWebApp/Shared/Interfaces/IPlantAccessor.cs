using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Shared.Interfaces
{
    public interface IPlantAccessor
    {
        Task<IEnumerable<Plant>> GetPlantsAsync();
        Task<Plant?> GetPlantByIdAsync(int id);
    }
}
