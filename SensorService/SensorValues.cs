using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorService
{
    public class SensorValues
    {
        const double MAX_VALUE = 0.35;
        const double MIN_VALUE = 0.65;

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
            double? humidityRange = (MIN_VALUE - voltageRange) / (MIN_VALUE - MAX_VALUE);
            return humidityRange;
        }
    }
}
