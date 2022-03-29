using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Repository.Interfaces
{
    public interface ISensorRepository
    {
        Sensor? GetById(int id);
        IEnumerable<Sensor> GetAll();
    }
}
