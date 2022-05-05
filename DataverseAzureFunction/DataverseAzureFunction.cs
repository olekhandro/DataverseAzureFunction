using System;
using System.IO;
using System.Threading.Tasks;
using DataverseAzureFunction.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DataverseAzureFunction
{
    public static class DataverseAzureFunction
    {
        public const string CONNECTIONSTRING = @"AuthType=OAuth;
                         Url=https://org9f7928c0.crm4.dynamics.com/;
                         UserName=alexanderzakharov@ontrial.onmicrosoft.com;
                         Password=Iskander1!;
                         ClientId=;
                         RedirectUri=;
                         Prompt=Auto;
                         RequireNewInstance=True";

        
        [FunctionName("ProcessTimeRange")]
        public static async Task<IActionResult> ProcessTimeRangeAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var request = JsonConvert.DeserializeObject<ProcessTimeRangeRequest>(requestBody);

            if (request.StartOn > request.EndOn)
            {
                return new BadRequestObjectResult("Start date cannot be bigger than End date.");
            }

            return new OkObjectResult("Time range processed successfully");
        }
    }
}
