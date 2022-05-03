using Microsoft.EntityFrameworkCore;

using PlantMonitorWebApp.Repository.Interfaces;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Repository.Classes
{
    public class PlantRepository : IPlantRepository
    {
        readonly PlantAppContext _context;

        public PlantRepository(PlantAppContext context) => _context = context;

        public IEnumerable<Plant> GetAll()
        {
            return _context.Plants.Include(p => p.Sensor).AsEnumerable();
        }

        public Plant? GetById(int id)
        {
            return _context.Plants.Include(p => p.Sensor).Where(p => p.Id == id).FirstOrDefault();
        }

        public void Insert(Plant plant)
        {
            try
            {
                // The controller may also recreate an existing sensor object from the incoming request JSON.
                // That has to be replaced by the sensor object representing the entry already in the database.
                // We could use data transfer objects, which adds more overhead.
                // But that includes dealing with correct references to existing objects, which we now have to do here anyway.
                if (plant.Sensor is not null)
                {
                    Sensor dbSensor = _context.Sensors.Where(s => s.Id == plant.Sensor.Id).FirstOrDefault() ?? plant.Sensor;
                    plant.Sensor = dbSensor;
                }
                _context.Plants.Add(plant);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error inserting Plant with name {plant.Name}", ex);
            }
        }

        public void Update(Plant plant)
        {
            try
            {
                Plant dbPlant = _context.Plants.Where(s => s.Id == plant.Id).Single();
                dbPlant.Name = plant.Name;
                dbPlant.Description = plant.Description;
                dbPlant.Sensor = plant.Sensor;
                dbPlant.ImageUrl = plant.ImageUrl;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error updating Plant with id {plant.Id}", ex);
            }
        }

        public void Delete(int id)
        {
            try
            {
                Plant plant = _context.Plants.Where(s => s.Id == id).Single();
                _context.Plants.Remove(plant);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException($"Error deleting Plant with id {id}", ex);
            }
        }
    }
}