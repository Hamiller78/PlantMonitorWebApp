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
        private ISeedSource _seedSource;

        public MockDataSource(ISeedSource seedSource)
        {
            _seedSource = seedSource;
        } 

        public double GetCurrentValue()
        {
            // Return a value that declines over the course of a day from 1 to 0
            double seedValue = (double)_seedSource.GetSeedValue();
            double dailyDecline 
                = 1d + Math.Sin(-seedValue
                       / (24 * 60 * 60) * Math.PI / 2d);

            return dailyDecline;
        }
    }
}