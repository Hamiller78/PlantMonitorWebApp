using PlantMonitorWebApp.Client.ItemViews;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Client.TestData
{
    public static class TestPlantProvider
    {

        public static IEnumerable<PlantView> GetTestPlantConfigurations()
        {
            var plantViewList = new List<PlantView>
            {
                new PlantView("Judith", "Weihnachtsstern", "/Images/CactusPic.png", 1),
                new PlantView("Mister Nancy", "Palme", "/Images/CactusPic.png", 2),
                new PlantView("Harry", "Philodendron", "/Images/CactusPic.png", 1)
            };

            return plantViewList;
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
