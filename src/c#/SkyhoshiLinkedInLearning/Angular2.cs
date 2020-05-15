using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SkyhoshiLinkedInLibrary.Processors.SteamAPI;
using SkyhoshiLinkedInLibrary.Configuration.UserSecrets;
using SkyhoshiLinkedInLibrary.Data.Steam.Apps;
using ExecutionContext = System.Threading.ExecutionContext;

namespace SkyhoshiLinkedInLearning
{
    using static SkyhoshiLinkedInLibrary.Configuration.Bootstrap.ApplicationConfiguration<SkyhoshiLibrary>;
    
    public static class Angular2
    {
        [FunctionName("Angular2GamesList")]
        public static async Task<IActionResult> GamesList([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Angular2GamesList")] HttpRequest req, ILogger log)
        {
            SkyhoshiLibrary libraryConfiguration = new SkyhoshiLibrary();

            string strUri = "https://skyhoshilinkedinlearning.blob.core.windows.net/";
            string accountName = "skyhoshilinkedinlearning";
            string keyValue = "L45WcXwqEkYj8jTkTbg0BhrPC3/DmFV9lZHjFXDleFayO8fECCj/B/ikFk15IiV5NhWkpcaLXO9xXYz+9CyM5w==";
            string containerName = "steamdata";
            string blobName = "2020-05 MAY/2020-05-14-1345.GetAppsList.json";
            string blobUri = "";

            //strUri = libraryConfiguration.AzureSettings.AzureStorage.Location;
            //accountName = libraryConfiguration.AzureSettings.AzureStorage.AccountName;
            //keyValue = libraryConfiguration.AzureSettings.AzureStorage.Key;
            //containerName = libraryConfiguration.AzureSettings.AzureStorage.PrimaryContainerName;
            //blobName = libraryConfiguration.AzureSettings.AzureStorage.PrimaryBlobName;
            blobUri = $@"{strUri}{containerName}/{blobName}";
            //HttpContext context = new DefaultHttpContext();
            
            EventId id = new EventId((new Random(3929).Next()), "GamesListResultEventId");
            log.LogInformation(id, $"Location:{strUri}, AccountName: {accountName}, KeyValue: {keyValue}, Container Name: {containerName}, BlobName: {blobName}, BlobURI: {blobUri}", null);

            MemoryStream memoryStream = new MemoryStream();
            CloudBlobClient cloudClient = new CloudBlobClient(new Uri(strUri), new StorageCredentials(accountName, keyValue), null);
            StorageUri blobStorageUri = new StorageUri(new Uri(blobUri));
            AccessCondition accessCondition = AccessCondition.GenerateEmptyCondition();
            BlobRequestOptions blobRequestOptions = new BlobRequestOptions();
            ICloudBlob blob = cloudClient.GetBlobReferenceFromServer(blobStorageUri, accessCondition, blobRequestOptions);
            blob.DownloadToStream(memoryStream);

            SteamFile steamFile = new SteamFile(memoryStream);
            string strJson =  steamFile.GetStringFromContentStream();
            SteamAppsList GameList = new SteamAppsList(strJson);
            return new JsonResult(GameList);
        }
        [FunctionName("Angular2GamesInfo")]
        public static async Task<IActionResult> GameInfo(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            //log.LogInformation("C# HTTP trigger function processed a request.");

            //string name = req.Query["name"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";

            //return new OkObjectResult(responseMessage);
            return new OkResult();
        }
    }
}
