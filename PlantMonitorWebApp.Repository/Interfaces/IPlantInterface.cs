using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Repository.Interfaces
{
    public interface IPlantInterface
    {
        Plant? GetById(int id);
        IEnumerable<Plant> GetAll();
    }
}
