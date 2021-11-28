using System;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

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
            DateTime currentTime = DateTime.Now;

            // Return a value that declines over the course of a day from 1 to 0
            double dailyDecline = 1d + Math.Sin(-currentTime.TimeOfDay.TotalSeconds / (24 * 60 * 60) * Math.PI / 2d);

            return dailyDecline;
        }
    }

}
