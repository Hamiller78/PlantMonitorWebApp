using Microsoft.AspNetCore.SignalR;
using PlantMonitorWebApp.Server.Interfaces;
using PlantMonitorWebApp.Server.Hubs;

namespace PlantMonitorWebApp.Server.Services.MessageSender
{
    public class SignalRSender : IMessageSender
    {
        private ILogger<SignalRSender> _logger;
        private IHubContext<SensorSignalRSender> _signalRHubContext;

        public SignalRSender(ILogger<SignalRSender> logger, IHubContext<SensorSignalRSender> signalRHub)
        {
            _logger = logger;
            _signalRHubContext = signalRHub;
        }

        public async Task SendSensorValueChanged(string sensorId, double value)
        {
            await _signalRHubContext.Clients.All.SendAsync("SensorValueChanged", sensorId, value);
        }
    }
}