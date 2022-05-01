using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantMonitorWebApp.Shared.Interfaces;

namespace PlantMonitorWebApp.Shared.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Uri? ServiceUri { get; set; }
    }
}
