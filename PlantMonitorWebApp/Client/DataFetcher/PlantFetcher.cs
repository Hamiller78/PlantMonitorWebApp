using System.Net.Http;
using System.Net.Http.Json;
using PlantMonitorWebApp.Client.Interfaces;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Client.DataFetcher
{
    public class PlantFetcher : IPlantFetcher
    {
        readonly HttpClient _client;

        public PlantFetcher(HttpClient client) => _client = client;

        public async Task<IEnumerable<Plant>> FetchPlantsAsync()
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
