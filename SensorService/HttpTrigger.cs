using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Net;
using System.Net.Http;

namespace SensorService
{
    public static class HttpTrigger
    {
        [FunctionName("GetValue")]
        public static async Task<HttpResponseMessage> GetValue(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetValue/{index}")] HttpRequestMessage req,
            [DurableClient] IDurableEntityClient client,
            int index)
        {
            var entityId = new EntityId("SensorValuesEntity", "StoredSensorValue");
            var state = await client.ReadEntityStateAsync<SensorValuesEntity>(entityId);

            if (!state.EntityExists)
            {
                return req.CreateResponse(HttpStatusCode.NotFound);
            }
            return req.CreateResponse(HttpStatusCode.OK, state.EntityState.CurrentValue.Values[index].Item1);
        }

        [FunctionName("SetValue")]
        public static async Task<HttpResponseMessage> IncrementCounter(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "SetValue")] HttpRequestMessage req,
            [DurableClient] IDurableEntityClient client)
        {
            string requestBody = await req.Content.ReadAsStringAsync();
            SensorValuesDto pureSensorValues = JsonConvert.DeserializeObject<SensorValuesDto>(requestBody);
            SensorValues timeStampedSensorValues = new(pureSensorValues);

            var entityId = new EntityId("SensorValuesEntity", "StoredSensorValue");

            // One-way signal to the entity - does not await a response
            await client.SignalEntityAsync(entityId, "Set", timeStampedSensorValues);

            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}
