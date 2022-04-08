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
            try
            {
                return _context.Sensors.AsEnumerable();
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error retrieving Sensor list from database", ex);
            }
        }

        public Sensor? GetById(int id)
        {
            try
            {
                return _context.Sensors.Where(s => s.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error retrieving Sensor with id {id}", ex);
            }
        }

        public void Insert(Sensor sensor)
        {
            try
            {
                _context.Sensors.Add(sensor);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error inserting Sensor with name {sensor.Name}", ex);
            }
        }

        public void Update(Sensor sensor)
        {
            try
            {
                Sensor dbSsensor = _context.Sensors.Where(s => s.Id == sensor.Id).Single();
                dbSsensor.Name = sensor.Name;
                dbSsensor.ServiceUri = sensor.ServiceUri;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error updating Sensor with id {sensor.Id}", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                Sensor sensor = _context.Sensors.Where(s => s.Id == id).Single();
                _context.Sensors.Remove(sensor);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error deleting Sensor with id {id}", ex);
            }
        }
    }
}
