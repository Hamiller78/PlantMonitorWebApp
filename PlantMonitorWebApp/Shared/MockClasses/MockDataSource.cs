using PlantMonitorWebApp.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitorWebApp.Shared.MockClasses
{
    public class MockDataSource : IDataSource
    {
        public double GetCurrentValue()
        {
            return 0d;
        }
    }
}
