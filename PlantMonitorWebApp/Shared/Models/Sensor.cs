using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantMonitorWebApp.Shared.Interfaces;

namespace PlantMonitorWebApp.Shared.Models
{
    public class Sensor : IDataSource
    {
        public int Id { get; set; } = 0;
        public double SensorValue { get; set; } = 0d;
        public string FormattedSensorValue => string.Format("{0,7:##0.000}%", 100 * SensorValue);
    }
}
