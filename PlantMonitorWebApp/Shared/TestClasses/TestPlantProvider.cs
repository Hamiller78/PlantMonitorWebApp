using PlantMonitorWebApp.Shared.Interfaces;
using PlantMonitorWebApp.Shared.Plants;

namespace PlantMonitorWebApp.Shared.TestClasses
{
    public static class TestPlantProvider
    {
        public static IEnumerable<PlantConfiguration> GetTestPlantConfigurations()
        {
            var plantList = new List<PlantConfiguration>
            {
                new PlantConfiguration() { Name = "Judith", Description = "Weihnachtsstern" },
                new PlantConfiguration() { Name = "Mister Nancy", Description = "Palme" },
                new PlantConfiguration() { Name = "Harry", Description = "Philodendron" }
            };

            return plantList;
        }
    }
}
