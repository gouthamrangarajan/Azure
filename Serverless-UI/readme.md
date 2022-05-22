### Serverless UI
#### Serverless function to render UI using Azure function
- an idea to send html response in Azure functions 
1. store html and corresponding js in separate folders
2. change settings of the above folders to be copied to output directory
3. based on incoming request url query params send the corresponding output
- C#
```C#
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
private async Task generateHtmlResponse(HttpResponseData response,string query)
{        
    response.Headers.Add("Content-Type", "text/html; charset=utf-8");
    var html=await System.IO.File.ReadAllTextAsync($"htmls/{query.Replace("?page=","")}.html");
    await response.WriteStringAsync(html);        
}
private async Task generateJsResponse(HttpResponseData response,string query)
{
    response.Headers.Add("Content-Type", "text/javascript");
    var js=await System.IO.File.ReadAllTextAsync($"js/{query.Replace("?js=","")}.js");
    await response.WriteStringAsync(js);
}
```
- csproj
```xml
...
<None Update="htmls\*">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    <CopyToPublishDirectory>PreserveNewest</CopyToPublishDirectory>
</None>
<None Update="js\*">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    <CopyToPublishDirectory>PreserveNewest</CopyToPublishDirectory>
</None>
</ItemGroup>
```
- sample to access javascript from html <script src="?js=another"></script>

- Sample Screenshot
  ![Screenshot](https://github.com/gouthamrangarajan/Azure/blob/main/Serverless-UI/screenshot.jpg)