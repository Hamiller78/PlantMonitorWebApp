using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Client.ItemViews
{
    public class PlantView
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public string FormattedSensorValue => string.Format("{0,7:##0.000}%", 100 * SensorValue);
        public int SensorId { get; set; }
        public double SensorValue { get; set; }  // Make this private?

        public PlantView(Plant plant)
        {
            Name = plant.Name;
            Description = plant.Description;
            ImageUrl = plant.ImageUrl;
            SensorId = plant.Sensor.Id;
        }

    }
}
