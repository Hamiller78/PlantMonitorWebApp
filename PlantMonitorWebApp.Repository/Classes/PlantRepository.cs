using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlantMonitorWebApp.Repository.Interfaces;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Repository.Classes
{
    public class PlantRepository : IPlantInterface
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
    }
}