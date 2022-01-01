using PlantMonitorWebApp.Shared.Interfaces;
using PlantMonitorWebApp.Shared.MockClasses;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Shared.TestClasses
{
    public static class TestPlantProvider
    {
        public static IEnumerable<Plant> GetTestPlantConfigurations()
        {
            IDataSource sensorDataSource = new MockDataSource(new SecondsOfDaySeedSource());
            var plantList = new List<Plant>
            {
                new Plant(sensorDataSource)
                {
                    Name = "Judith",
                    Description = "Weihnachtsstern",
                    ImageUrl = "/Images/CactusPic.png"
                },
                new Plant(sensorDataSource)
                {
                    Name = "Mister Nancy",
                    Description = "Palme",
                    ImageUrl = "/Images/CactusPic.png"
                },
                new Plant(sensorDataSource)
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
