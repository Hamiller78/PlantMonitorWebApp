using PlantMonitorWebApp.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitorWebApp.Shared.DataSources
{
    public class RestDataSource : IDataSource
    {
        public double SensorValue { get => ReadFromRestService(); }

        private Uri _restServiceUri;
        public RestDataSource(Uri restServiceUri)
        {
            _restServiceUri = restServiceUri;
        }

        private double ReadFromRestService()
        {
            HttpClient client = new();
            Task<HttpResponseMessage> response = client.GetAsync(_restServiceUri);
            string resultString = response.Result.Content.ReadAsStringAsync().Result;
            double value = double.Parse(resultString, CultureInfo.InvariantCulture);
            return value;
        }
    }
}