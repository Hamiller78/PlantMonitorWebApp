using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PlantMonitorWebApp.Server.Interfaces;

namespace PlantMonitorWebApp.Server.Hubs
{
    public class SensorValueHub : Hub, IMessageSender
    {
        public async Task SendSensorValueChanged(string sensorId, double value)
        {
            await Clients.All.SendAsync("SensorValueChanged", sensorId, value);
        }
    }
}