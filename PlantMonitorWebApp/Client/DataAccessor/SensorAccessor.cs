using System.Net.Http.Json;
using PlantMonitorWebApp.Client.Interfaces;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Client.DataAccessor
{
    public class SensorAccessor : ISensorAccessor
    {
        readonly HttpClient _client;

        public SensorAccessor(HttpClient client) => _client = client;

        public async Task<IEnumerable<Sensor>> GetSensorsAsync()
        {
            var sensors = await _client.GetFromJsonAsync<IEnumerable<Sensor>>("Sensors/GetList") ?? new List<Sensor>();
            return sensors;
        }

        public async Task<Sensor?> GetSensorByIdAsync(int id)
        {
            var sensor = await _client.GetFromJsonAsync<Sensor>($"Sensors/Item/{id.ToString().Trim()}");
            return sensor;
        }

        public async Task<HttpResponseMessage> CreateSensorAsync(Sensor sensor)
        {
            var response = await _client.PostAsJsonAsync($"Sensors/Insert", sensor);
            return response;
        }

        public async Task<HttpResponseMessage> UpdateSensorAsync(Sensor sensor)
        {
            var response = await _client.PostAsJsonAsync($"Sensors/Update", sensor);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteSensorAsync(int id)
        {
            var response = await _client.DeleteAsync($"Sensors/Delete/{id.ToString().Trim()}");
            return response;
        }
    }
}
