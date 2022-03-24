using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Client.ItemViews
{
    public class PlantView
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public int SensorId { get; set; } = 0;
        public string FormattedSensorValue => string.Format("{0,7:##0.000}%", 100 * SensorValue);
        public double SensorValue { get; set; }  // Make this private?

        public PlantView(Plant plant)
        {
            Name = plant.Name;
            Description = plant.Description;
            ImageUrl = plant.ImageUrl;
            SensorId = plant.Sensor.Id;
        }

        public PlantView(string name, string description, string imageUrl, int sensorId)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            SensorId = sensorId;
        }

    }
}
