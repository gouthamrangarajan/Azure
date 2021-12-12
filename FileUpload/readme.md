### Simple http triggered azure function to upload file

#### Function folder shows azure function code

#### official link to create azure function

[https://docs.microsoft.com/en-us/azure/azure-functions/create-first-function-vs-code-csharp?tabs=in-process](https://docs.microsoft.com/en-us/azure/azure-functions/create-first-function-vs-code-csharp?tabs=in-process)

#### code changes

```C#
 if(req.Form==null || req.Form.Files==null || req.Form.Files.Count==0
                    || req.Form.Files["file"]==null
                     )
                    throw new Exception("Bad Request");


var file = req.Form.Files["file"];
var ms=new MemoryStream();
await file.CopyToAsync(ms);
return new FileContentResult(ms.ToArray(),file.ContentType);

```

#### client folder shows sample client using Vite and Vue client

```html
<script id="upload" type="text/template">
  <transition name="slide-down" appear>
    <form class="bg-white rounded-xl py-4 px-6 mt-24 shadow-2xl flex flex-col space-y-3"
      action="http://localhost:7071/api/FileUpload" method="post" enctype="multipart/form-data"
      >
      <span class="text-blue-700 font-semibold text-lg">File Upload</span>
      <div class="flex space-x-3">
        <button type="button" class="appearance-none outline-none py-2 px-3 rounded-lg
              bg-indigo-600 hover:opacity-90 focus:ring-2 focus:ring-offset-2 focus:ring-offset-indigo-100
              focus:ring-indigo-400 text-white transition duration-300 text-lg"
              @click.stop="()=>uploadRef.click()">
            Select File
        </button>
        <button type="submit"
          :class="['appearance-none outline-none py-2 px-3 rounded-lg  bg-green-600 hover:opacity-90 focus:ring-2 focus:ring-offset-2 focus:ring-offset-green-100  focus:ring-green-400 text-white text-lg transition duration-300',
                  {'cursor-pointer':fileName!=''},{'cursor-not-allowed':fileName==''}]"
            :disabled="fileName==''">
            Convert
      </button>
      </div>
      ....
```

- Sample Screenshot
  ![Screenshot](https://github.com/gouthamrangarajan/Azure/blob/main/FileUpload/screenshot.jpg)
