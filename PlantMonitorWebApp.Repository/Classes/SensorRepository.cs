using Microsoft.EntityFrameworkCore;

using PlantMonitorWebApp.Repository.Interfaces;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Repository.Classes
{
    public class SensorRepository : ISensorRepository
    {
        readonly PlantAppContext _context;

        public SensorRepository(PlantAppContext context) => _context = context;

        public IEnumerable<Sensor> GetAll()
        {
            return _context.Sensors.AsEnumerable();
        }

        public Sensor? GetById(int id)
        {
            return _context.Sensors.Where(s => s.Id == id).FirstOrDefault();
        }
    }
}
