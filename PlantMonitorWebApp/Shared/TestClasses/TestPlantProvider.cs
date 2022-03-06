using PlantMonitorWebApp.Shared.Interfaces;
using PlantMonitorWebApp.Shared.MockClasses;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Shared.TestClasses
{
    public static class TestPlantProvider
    {

        public static IEnumerable<Plant> GetTestPlantConfigurations()
        {
            List<Sensor> sensorList = GetTestSensors();

            var plantList = new List<Plant>
            {
                new Plant(sensorList[1])
                {
                    Name = "Judith",
                    Description = "Weihnachtsstern",
                    ImageUrl = "/Images/CactusPic.png"
                },
                new Plant(sensorList[2])
                {
                    Name = "Mister Nancy",
                    Description = "Palme",
                    ImageUrl = "/Images/CactusPic.png"
                },
                new Plant(sensorList[1])
                {
                    Name = "Harry",
                    Description = "Philodendron",
                    ImageUrl = "/Images/CactusPic.png"
                }
            };

            return plantList;
        }

        public static List<Sensor> GetTestSensors()
        {
            return new List<Sensor>()
            {
                new Sensor(),
                new Sensor()
            };
        }
    }
}
