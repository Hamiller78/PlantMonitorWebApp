using PlantMonitorWebApp.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitorWebApp.Shared.MockClasses
{
    public class MockSinePerMinuteData : IDataSource
    {
        double IDataSource.SensorValue
        {
            get { return GetCurrentValue(); }
            set { }
        }

        private readonly ISeedSource _seedSource;

        public MockSinePerMinuteData(ISeedSource seedSource)
        {
            _seedSource = seedSource;
        }

        public double GetCurrentValue()
        {
            // Return a value that oscillates within a minute
            double seedValue = (double)_seedSource.GetSeedValue();
            double minuteOscillation
                = 0.5d + 0.5d * Math.Sin(seedValue / 60 * 2d * Math.PI);

            return minuteOscillation;
        }
    }
}
