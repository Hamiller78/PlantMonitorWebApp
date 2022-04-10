using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Repository.Interfaces
{
    public interface IPlantRepository
    {
        Plant? GetById(int id);
        IEnumerable<Plant> GetAll();
        void Insert(Plant plant);
        void Update(Plant plant);
        void Delete(int id);
    }
}
