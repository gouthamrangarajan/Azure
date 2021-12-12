using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace functions
{
    public static class FileUpload
    {
        [FunctionName("FileUpload")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
             log.LogInformation($"Azure Function to Upload Document called {DateTime.Now.ToString("yyyy/MM/dd HH:mm ")}.");   
           
            try
            {
                if(req.Form==null || req.Form.Files==null || req.Form.Files.Count==0
                    || req.Form.Files["file"]==null            
                     )
                    throw new Exception("Bad Request");
                

                var file = req.Form.Files["file"];                                                             
                var ms=new MemoryStream();
                await file.CopyToAsync(ms);
                //Sample to Send back the content
                //can be extended to be uploaded to drive/blob storage after additional parsing/processing
                return new FileContentResult(ms.ToArray(),file.ContentType);
            }
            catch(Exception e)
            {
                log.LogError($"Error in Azure function while uploading document",e);  
                log.LogInformation(e.Message);                              
                return new BadRequestObjectResult(new{IsError=true,ErrorMsg="Invalid Request"});
            }
        }
    }
}
