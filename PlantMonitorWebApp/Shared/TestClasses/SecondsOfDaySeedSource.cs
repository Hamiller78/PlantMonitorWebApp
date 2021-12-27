using PlantMonitorWebApp.Shared.Interfaces;

namespace PlantMonitorWebApp.Shared.TestClasses
{
    public class SecondsOfDaySeedSource : ISeedSource
    {
        long ISeedSource.GetSeedValue()
        {
            return (long)DateTime.Now.TimeOfDay.TotalSeconds;
        }
    }
}