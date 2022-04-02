using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Client.Interfaces
{
    public interface IPlantFetcher
    {
        Task<IEnumerable<Plant>> FetchPlantsAsync();
        Task<Plant?> GetPlantByIdAsync(int id);
    }
}
