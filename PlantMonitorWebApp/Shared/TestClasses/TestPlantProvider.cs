using PlantMonitorWebApp.Shared.Interfaces;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Shared.TestClasses
{
    public static class TestPlantProvider
    {
        public static IEnumerable<Plant> GetTestPlantConfigurations()
        {
            var plantList = new List<Plant>
            {
                new Plant() { Name = "Judith", Description = "Weihnachtsstern", ImageUrl = "/Images/CactusPic.png" },
                new Plant() { Name = "Mister Nancy", Description = "Palme", ImageUrl = "/Images/CactusPic.png"},
                new Plant() { Name = "Harry", Description = "Philodendron", ImageUrl = "/Images/CactusPic.png" }
            };

            return plantList;
        }
    }
}
