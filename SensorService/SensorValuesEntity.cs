using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SensorService
{
    public class SensorValuesEntity
    {
        const int VALUE_EXPIRATION_IN_MINUTES = 60;

        [JsonProperty("value")]
        public SensorValues CurrentValue { get; set; }

        public void Set(SensorValues amount) => this.CurrentValue = amount;

        [FunctionName(nameof(SensorValuesEntity))]
        public static Task Run([EntityTrigger] IDurableEntityContext ctx)
            => ctx.DispatchAsync<SensorValuesEntity>();
    }
}