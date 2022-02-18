namespace PlantMonitorWebApp.Server.Interfaces
{
    public interface IDataUpdater
    {
        Task RefreshData(object? state);
    }
}
