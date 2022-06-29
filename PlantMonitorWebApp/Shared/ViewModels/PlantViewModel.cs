using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Shared.ViewModels;

public class PlantViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int ImageId { get; set; } = -1;
    public string? ImageUrl { get; set; } = null;
    public int SensorId { get; set; } = -1;
    public string SensorName { get; set; } = string.Empty;
    public string FormattedSensorValue => string.Format("{0,7:##0.000}%", 100 * SensorValue);
    public double SensorValue { get; set; }  // Make this private?
    public int AlertLevel { get; set; } = 0;
    public bool IsAlertEnabled { get; set; } = false;

    public PlantViewModel() { }

    public PlantViewModel(Plant plant)
    {
        Id = plant.Id;
        Name = plant.Name;
        Description = plant.Description;
        ImageId = plant.StoredImage?.Id ?? -1;
        string? fileName = plant.StoredImage?.FileName ?? null;
        ImageUrl = fileName is not null ? "/Images/Fetch?id=" + fileName : null;
        SensorId = plant.Sensor?.Id ?? -1;
        SensorName = plant.Sensor?.Name ?? string.Empty;
        AlertLevel = plant.AlertLevel;
        IsAlertEnabled = plant.IsAlertEnabled;
    }

    public PlantViewModel DeepCopy()
    {
        PlantViewModel clone = (PlantViewModel)this.MemberwiseClone();

        return clone;
    }

    public void SetValuesFrom(PlantViewModel sourceVM)
    {
        Name = sourceVM.Name;
        Description = sourceVM.Description;
        ImageId = sourceVM.ImageId;
        SensorId = sourceVM.SensorId;
        SensorName = sourceVM.SensorName;
        AlertLevel = sourceVM.AlertLevel;
        IsAlertEnabled = sourceVM.IsAlertEnabled;
    }

    public Plant ToPlant(IEnumerable<Sensor> sensors, IEnumerable<StoredImage> images)
    {
        return new Plant()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            StoredImage = images.Where(i => i.Id == ImageId).FirstOrDefault(),
            Sensor = sensors.Where(s => s.Id == SensorId).FirstOrDefault(),
            AlertLevel = AlertLevel,
            IsAlertEnabled = IsAlertEnabled
        };
    }
}
