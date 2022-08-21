using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitorWebApp.Shared.Models
{
    public class LatestSensorValue
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTime TimeReceived { get; set; }
    }
}
