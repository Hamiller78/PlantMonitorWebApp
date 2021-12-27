using System;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

using PlantMonitorWebApp.Shared.MockClasses;
using PlantMonitorWebApp.Shared.TestClasses;

namespace MockSensorService
{
    public static class GetSensorValue
    {
        [Function("GetSensorValue")]
        public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("GetSensorValue");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString($"value={GetFakeValue()}");

            return response;
        }

        private static double GetFakeValue()
        {
            var valueSource = new MockDataSource(new SecondsOfDaySeedSource());

            return valueSource.GetCurrentValue();
        }
    }

}
