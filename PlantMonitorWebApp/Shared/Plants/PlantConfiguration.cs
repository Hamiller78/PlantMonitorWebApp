using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlantMonitorWebApp.Shared.Interfaces;

namespace PlantMonitorWebApp.Shared.Plants
{
    public class PlantConfiguration
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        // public Image Icon { get; set; }
        //public IDataSource SensorSource { get; set; } = null;
    }
}
