using System.Net.Http;
using System.Net.Http.Json;
using PlantMonitorWebApp.Shared.Interfaces;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Shared.DataAccessor
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
            var plant = await _client.GetFromJsonAsync<Plant>($"Plants/Item/{id.ToString().Trim()}");
            return plant;
        }

        public async Task<HttpResponseMessage> CreatePlantAsync(Plant plant)
        {
            var response = await _client.PostAsJsonAsync($"Plants/Insert", plant);
            return response;
        }

        public async Task<HttpResponseMessage> UpdatePlantAsync(Plant plant)
        {
            var response = await _client.PostAsJsonAsync($"Plants/Update", plant);
            return response;
        }

        public async Task<HttpResponseMessage> DeletePlantAsync(int id)
        {
            var response = await _client.DeleteAsync($"Plants/Delete/{id.ToString().Trim()}");
            return response;
        }
    }
}
