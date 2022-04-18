using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Shared.ViewModels
{
    public class SensorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UriString { get; set; }

        public SensorViewModel()
        {
            Name = string.Empty;
            UriString = string.Empty;
        }

        public SensorViewModel(Sensor sensor)
        {
            Id = sensor.Id;
            Name = sensor.Name;
            UriString = sensor.ServiceUri?.ToString() ?? string.Empty;
        }

        public Sensor ToSensor()
        {
            return new Sensor() { Id = Id, Name = Name, ServiceUri = new Uri(UriString ?? string.Empty) };
        }
    }
}
