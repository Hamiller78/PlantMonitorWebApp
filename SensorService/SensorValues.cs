using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorService
{
    public class SensorValues
    {
        public (double?, DateTime)[] Values { get; set; }

        public SensorValues() { } // Without this empty constructor the framework calls the other constructor with a null value
        public SensorValues(SensorValuesDto incomingData)
        {
            try
            {
                Values = incomingData.Values
                            .Select(v => ((double?)Convert.ToDouble(v) / short.MaxValue, DateTime.Now))
                            .ToArray();
            }
            catch
            {
                Values = new(double?, DateTime)[] 
                         {
                             (null, DateTime.Now),
                             (null, DateTime.Now),
                             (null, DateTime.Now),
                             (null, DateTime.Now)
                         };                        ;
            }
        }
    }
}
