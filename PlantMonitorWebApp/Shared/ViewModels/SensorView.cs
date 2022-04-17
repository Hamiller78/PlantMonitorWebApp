using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Shared.ViewModels
{
    public class SensorView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UriString { get; set; }

        public SensorView(Sensor sensor)
        {
            Id = sensor.Id;
            Name = sensor.Name;
            UriString = sensor.ServiceUri?.ToString() ?? string.Empty;
        }

        public Sensor ToSensor()
        {
            return new Sensor() { Name = Name, ServiceUri = new Uri(UriString ?? string.Empty) };
        }
    }
}
