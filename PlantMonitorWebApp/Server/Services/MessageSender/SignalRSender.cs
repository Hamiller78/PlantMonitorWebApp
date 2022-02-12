using PlantMonitorWebApp.Server.Interfaces;
using PlantMonitorWebApp.Server.Hubs;

namespace PlantMonitorWebApp.Server.Services.MessageSender
{
    public class SignalRSender : IMessageSender
    {
        private ILogger _logger;
        private SensorValueHub _signalRHub;

        public SignalRSender(ILogger logger, SensorValueHub signalRHub)
        {
            _logger = logger;
            _signalRHub = signalRHub;
        }

        public async Task SendSensorValueChanged(string sensorId, double value)
        {
            await _signalRHub.SendSensorValueChanged(sensorId, value);
        }
    }
}
