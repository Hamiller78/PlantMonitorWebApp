using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantMonitorWebApp.Shared.Interfaces;

namespace PlantMonitorWebApp.Shared.Models
{
    public class Sensor : IDataSource
    {
        public int Id { get; set; } = 0;
        public Guid SensorId { get; set; }
        public Uri? ServiceUri { get; set; }
        [NotMapped]
        public double SensorValue { get; set; }
        [NotMapped]
        public string FormattedSensorValue => string.Format("{0,7:##0.000}%", 100 * SensorValue);
    }
}
