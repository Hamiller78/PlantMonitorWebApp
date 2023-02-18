using System.Net.Http.Json;
using PlantMonitorWebApp.Client.Interfaces;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Client.DataAccessor
{
    public class SensorAccessor : ISensorAccessor
    {
        readonly HttpClient _anonymousClient;
        readonly HttpClient _authorizedClient;

        public SensorAccessor(IHttpClientFactory clientFactory)
        {
            _anonymousClient = clientFactory.CreateClient("ServerAPI.AnonymousAccess");
            _authorizedClient = clientFactory.CreateClient("ServerAPI.AuthenticatedAccess");
        }

        public async Task<IEnumerable<Sensor>> GetSensorsAsync()
        {
            var sensors = await _anonymousClient.GetFromJsonAsync<IEnumerable<Sensor>>("Sensors/GetList") ?? new List<Sensor>();
            return sensors;
        }

        public async Task<Sensor?> GetSensorByIdAsync(int id)
        {
            var sensor = await _anonymousClient.GetFromJsonAsync<Sensor>($"Sensors/Item/{id.ToString().Trim()}");
            return sensor;
        }

        public async Task<HttpResponseMessage> CreateSensorAsync(Sensor sensor)
        {
            var response = await _authorizedClient.PostAsJsonAsync($"Sensors/Insert", sensor);
            return response;
        }

        public async Task<HttpResponseMessage> UpdateSensorAsync(Sensor sensor)
        {
            var response = await _authorizedClient.PostAsJsonAsync($"Sensors/Update", sensor);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteSensorAsync(int id)
        {
            var response = await _authorizedClient.DeleteAsync($"Sensors/Delete/{id.ToString().Trim()}");
            return response;
        }
    }
}
