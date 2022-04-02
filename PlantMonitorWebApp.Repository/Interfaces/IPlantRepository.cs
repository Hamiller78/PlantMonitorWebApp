using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Repository.Interfaces
{
    public interface IPlantRepository
    {
        Plant? GetById(int id);
        IEnumerable<Plant> GetAll();
    }
}
