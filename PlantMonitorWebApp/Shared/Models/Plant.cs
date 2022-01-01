using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlantMonitorWebApp.Shared.Interfaces;

namespace PlantMonitorWebApp.Shared.Models
{
    public class Plant
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string ImageUrl { get; set; } = "";

        public double SensorValue => _sensorSource?.GetCurrentValue() ?? 0d;
        public string FormattedSensorValue => string.Format("{0,7:##0.000}%", 100 * SensorValue);

        private readonly IDataSource? _sensorSource = null;

        public Plant(IDataSource sensorSource)
        {
            _sensorSource = sensorSource;
        }
    }
}
