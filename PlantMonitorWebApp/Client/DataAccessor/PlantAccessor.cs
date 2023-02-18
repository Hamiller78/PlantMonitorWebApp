using System.Net.Http;
using System.Net.Http.Json;
using PlantMonitorWebApp.Client.Interfaces;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Client.DataAccessor
{
    public class PlantAccessor : IPlantAccessor
    {
        readonly HttpClient _anonymousClient;
        readonly HttpClient _authorizedClient;

        public PlantAccessor(IHttpClientFactory clientFactory)
        {
            _anonymousClient = clientFactory.CreateClient("ServerAPI.AnonymousAccess");
            _authorizedClient = clientFactory.CreateClient("ServerAPI.AuthenticatedAccess");
        }

        public async Task<IEnumerable<Plant>> GetPlantsAsync()
        {
            var plants = await _anonymousClient.GetFromJsonAsync<IEnumerable<Plant>>("Plants/GetList") ?? new List<Plant>();
            return plants;
        }

        public async Task<Plant?> GetPlantByIdAsync(int id)
        {
            var plant = await _anonymousClient.GetFromJsonAsync<Plant>($"Plants/Item/{id.ToString().Trim()}");
            return plant;
        }

        public async Task<HttpResponseMessage> CreatePlantAsync(Plant plant)
        {
            var response = await _authorizedClient.PostAsJsonAsync($"Plants/Insert", plant);
            return response;
        }

        public async Task<HttpResponseMessage> UpdatePlantAsync(Plant plant)
        {
            var response = await _authorizedClient.PostAsJsonAsync($"Plants/Update", plant);
            return response;
        }

        public async Task<HttpResponseMessage> DeletePlantAsync(int id)
        {
            var response = await _authorizedClient.DeleteAsync($"Plants/Delete/{id.ToString().Trim()}");
            return response;
        }
    }
}
