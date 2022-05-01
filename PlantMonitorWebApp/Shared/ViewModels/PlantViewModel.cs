using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Shared.ViewModels;

public class PlantViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public int SensorId { get; set; } = -1;
    public string SensorName { get; set; } = string.Empty;
    public string FormattedSensorValue => string.Format("{0,7:##0.000}%", 100 * SensorValue);
    public double SensorValue { get; set; }  // Make this private?

    public PlantViewModel() { }

    public PlantViewModel(Plant plant)
    {
        Id = plant.Id;
        Name = plant.Name;
        Description = plant.Description;
        ImageUrl = plant.ImageUrl;
        SensorId = plant.Sensor?.Id ?? -1;
        SensorName = plant.Sensor?.Name ?? string.Empty;
    }

    public PlantViewModel(string name, string description, string imageUrl, int sensorId)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        SensorId = sensorId;
    }

    public Plant ToPlant(IEnumerable<Sensor> sensors)
    {
        return new Plant()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            ImageUrl = ImageUrl,
            Sensor = sensors.Where(s => s.Id == SensorId).FirstOrDefault()
        };
    }
}
