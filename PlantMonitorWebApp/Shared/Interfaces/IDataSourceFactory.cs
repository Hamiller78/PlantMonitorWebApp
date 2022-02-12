using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitorWebApp.Shared.Interfaces
{
    public interface IDataSourceFactory
    {
        IDataSource CreateDataSource(object parameter);
    }
}
