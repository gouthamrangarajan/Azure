using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace UIFn
{
    public class UI
    {
        private readonly ILogger _logger;

        public UI(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<UI>();
        }

        [Function("UI")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var response = req.CreateResponse(HttpStatusCode.OK);
            var query=req.Url.Query;
            await generateResponse(response,query);               
            return response;
        }
        private Task generateResponse(HttpResponseData response,string query)
        =>
            (query.Contains("?js="),query.Contains("?page="))switch{
                 (true,_)=>generateJsResponse(response,query),
                 (_,true)=>generateHtmlResponse(response,query),
                 (_,_)=> generateLandingPageResponse(response),
             };
                                      
        private async Task generateJsResponse(HttpResponseData response,string query)
        {
            response.Headers.Add("Content-Type", "text/javascript");
            var js=await System.IO.File.ReadAllTextAsync($"js/{query.Replace("?js=","")}.js");
            await response.WriteStringAsync(js);
        }
        private async Task generateHtmlResponse(HttpResponseData response,string query)
        {        
            response.Headers.Add("Content-Type", "text/html; charset=utf-8");
            var html=await System.IO.File.ReadAllTextAsync($"htmls/{query.Replace("?page=","")}.html");
            await response.WriteStringAsync(html);        
        }
        private async Task generateLandingPageResponse(HttpResponseData response)
        {        
            response.Headers.Add("Content-Type", "text/html; charset=utf-8");
            var html=await System.IO.File.ReadAllTextAsync("htmls/landing.html");
            await response.WriteStringAsync(html);        
        }
    }    
}
