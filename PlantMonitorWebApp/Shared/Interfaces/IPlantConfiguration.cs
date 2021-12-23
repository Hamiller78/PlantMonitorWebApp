using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace PlantMonitorWebApp.Shared.Interfaces
{
    public interface IPlantConfiguration
    {
        public string Name { get; set; }
        public Image Icon { get; set; }
        public IDataSource SensorSource { get; set; }

    }
}
