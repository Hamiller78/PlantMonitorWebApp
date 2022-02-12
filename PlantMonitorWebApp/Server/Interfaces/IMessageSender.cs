namespace PlantMonitorWebApp.Server.Interfaces
{
    public interface IMessageSender
    {
        Task SendSensorValueChanged(string sensorId, double value);
    }
}
