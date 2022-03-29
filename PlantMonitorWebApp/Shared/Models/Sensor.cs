﻿using System;
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
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public Uri ServiceUri { get; set; }
    }
}
