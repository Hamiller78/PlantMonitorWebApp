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
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetValue/{entityKey}")] HttpRequestMessage req,
            [DurableClient] IDurableEntityClient client,
            string entityKey)
        {
            var entityId = new EntityId("Counter", entityKey);
            var state = await client.ReadEntityStateAsync<Counter>(entityId);

            if (!state.EntityExists)
            {
                return req.CreateResponse(HttpStatusCode.NotFound);
            }
            return req.CreateResponse(HttpStatusCode.OK, state.EntityState.CurrentValue.ToString());
        }

        [FunctionName("SetValue")]
        public static async Task<HttpResponseMessage> IncrementCounter(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "SetValue/{entityKey}/{value}")] HttpRequestMessage req,
            [DurableClient] IDurableEntityClient client,
            string entityKey,
            int value)
        {
            var entityId = new EntityId("Counter", entityKey.ToString().Trim());

            // One-way signal to the entity - does not await a response
            await client.SignalEntityAsync(entityId, "Set", value);

            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}
