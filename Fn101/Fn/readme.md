### Azure Function

```C#
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
```

#### official link

[https://docs.microsoft.com/en-us/azure/azure-functions/create-first-function-vs-code-csharp](https://docs.microsoft.com/en-us/azure/azure-functions/create-first-function-vs-code-csharp)
