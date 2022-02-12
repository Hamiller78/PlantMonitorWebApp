using PlantMonitorWebApp.Shared.DataSources;
using PlantMonitorWebApp.Shared.Interfaces;

namespace PlantMonitorWebApp.Shared.Factories
{
    public class RestDataSourceFactory : IDataSourceFactory
    {
        public IDataSource CreateDataSource(object parameter)
        {
            Uri? restUri = parameter as Uri;
            if (restUri == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            };
            return new RestDataSource(restUri);
        }
    }
}
