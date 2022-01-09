using PlantMonitorWebApp.Shared.Interfaces;
using PlantMonitorWebApp.Shared.MockClasses;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Shared.TestClasses
{
    public static class TestPlantProvider
    {
        public static IEnumerable<Plant> GetTestPlantConfigurations()
        {
            // IDataSource sensorDataSource = new MockDailyData(new SecondsOfDaySeedSource());
            // IDataSource sensorDataSource2 = new MockSinePerMinuteData(new SecondsOfDaySeedSource());
            Sensor sensor1 = new Sensor() { SensorId = 1 };
            Sensor sensor2 = new Sensor() { SensorId = 2 };

            var plantList = new List<Plant>
            {
                new Plant(sensor1)
                {
                    Name = "Judith",
                    Description = "Weihnachtsstern",
                    ImageUrl = "/Images/CactusPic.png"
                },
                new Plant(sensor2)
                {
                    Name = "Mister Nancy",
                    Description = "Palme",
                    ImageUrl = "/Images/CactusPic.png"
                },
                new Plant(sensor1)
                {
                    Name = "Harry",
                    Description = "Philodendron",
                    ImageUrl = "/Images/CactusPic.png"
                }
            };

            return plantList;
        }
    }
}
