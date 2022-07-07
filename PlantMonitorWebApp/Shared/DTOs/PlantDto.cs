using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Shared.DTOs
{
    public class PlantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int? SensorId { get; set; } 

        public PlantDto(Plant plant)
        {
            Id = plant.Id;
            Name = plant.Name;
            Description = plant.Description;
            ImageUrl = plant.ImageUrl;
            SensorId = plant.SensorId;
        }

    }
}
