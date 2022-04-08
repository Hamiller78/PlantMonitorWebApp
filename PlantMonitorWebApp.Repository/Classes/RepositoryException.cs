using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitorWebApp.Repository.Classes
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string message, Exception exception) : base(message, exception) { }
    }
}
