using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

using PlantMonitorWebApp.Shared.Interfaces;

namespace PlantMonitorWebApp.Shared.Models
{
    public class Plant
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public Sensor? Sensor { get; set; }
    }
}
