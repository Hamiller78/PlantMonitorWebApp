using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace PlantMonitorWebApp.Server.Hubs
{
    public class SensorValueHub : Hub
    {
        public async Task UpdateValue(string sensorId, double value)
        {
            await Clients.All.SendAsync("SensorValueChanged", sensorId, value);
        }
    }
}