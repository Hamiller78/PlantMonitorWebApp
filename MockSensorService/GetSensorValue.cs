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
        public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetSensorValue/{id}")] HttpRequestData req,
            string id,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("GetSensorValue");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            if (id == "1")
            {
                response.WriteString($"value={GetFakeValue()}");
            }
            else if (id == "2")
            {
                response.WriteString($"value={GetFakeValue2()}");
            }
            else
            {
                response.WriteString($"value=-1");
            }

            return response;
        }

        private static double GetFakeValue()
        {
            var valueSource = new MockDailyData(new SecondsOfDaySeedSource());

            return valueSource.GetCurrentValue();
        }

        private static double GetFakeValue2()
        {
            var valueSource = new MockSinePerMinuteData(new SecondsOfDaySeedSource());

            return valueSource.GetCurrentValue();
        }
    }

}
