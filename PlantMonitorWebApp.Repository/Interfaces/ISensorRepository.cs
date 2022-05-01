using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Repository.Interfaces
{
    public interface ISensorRepository
    {
        Sensor? GetById(int id);
        IEnumerable<Sensor> GetAll();
        void Insert(Sensor sensor);
        void Update(Sensor sensor);
        void Delete(int id);
    }
}
