using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitorWebApp.Shared.Interfaces
{
    public interface IDataSource
    {
        double SensorValue { get; set; }
    }
}
