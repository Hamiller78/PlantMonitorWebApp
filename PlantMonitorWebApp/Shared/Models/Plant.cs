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
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public Sensor Sensor { get; set; }

        public Plant() => Sensor = new Sensor();

        public Plant(Sensor sensor)
        {
            Sensor = sensor;
        }
    }
}
