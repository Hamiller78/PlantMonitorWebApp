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
                            .Select(v => (ConvertToScale(v), DateTime.Now))
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
                         };
            }
        }

        private double? ConvertToScale(int valueFromRaspberryPi)
        {
            double? voltageRange = Convert.ToDouble(valueFromRaspberryPi) / short.MaxValue;
            double? humidityRange = (0.45 - voltageRange) / (0.45 - 0.35); // scale 0.45-0.35 to 0-1
            return humidityRange;
        }
    }
}
