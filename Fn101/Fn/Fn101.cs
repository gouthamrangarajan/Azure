using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Fn101
{
    public class Fn101
    {
        private readonly ILogger _logger;

        public Fn101(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Fn101>();
        }

        [Function("Fn101")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Allow-Origin","*");
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");            
            response.WriteString($"Random Message {new Random().Next(1,9999)} from Azure Function");

            return response;
        }
    }
}
