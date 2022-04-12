using System.Net.Http;
using System.Net.Http.Json;
using PlantMonitorWebApp.Client.Interfaces;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Client.DataAccessor
{
    public class PlantAccessor : IPlantAccessor
    {
        readonly HttpClient _client;

        public PlantAccessor(HttpClient client) => _client = client;

        public async Task<IEnumerable<Plant>> GetPlantsAsync()
        {
            var plants = await _client.GetFromJsonAsync<IEnumerable<Plant>>("Plants/GetList") ?? new List<Plant>();
            return plants;
        }

        public async Task<Plant?> GetPlantByIdAsync(int id)
        {
            var plant = await _client.GetFromJsonAsync<Plant>($"Plant/Item/{id.ToString().Trim()}");
            return plant;
        }
    }
}
