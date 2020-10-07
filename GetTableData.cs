using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Memoryleek.FunctionCSharpUtils.Models;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Linq;
using System.Collections.Generic;

namespace Memoryleek.FunctionCSharpUtils
{
    public static class GetTableData
    {
        [FunctionName("GetTableData")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Getting Table Data");

            var accountName = GetEnvironmentVariable("STORAGE_ACCOUNT");
            var accountKey = GetEnvironmentVariable("STORAGE_KEY");
            var storageCreds = new StorageCredentials(accountName, accountKey);



            var storageAccount = new CloudStorageAccount(storageCreds,true);
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference("Demo");

            TableContinuationToken token = null;
            var entities = new List<GetTableDataItem>();

            do
            {
                var queryResult = await table.ExecuteQuerySegmentedAsync(new TableQuery<GetTableDataItem>(), token);
                entities.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;
            } while (token != null);
        

            return new OkObjectResult(entities.ToArray());
        }

        public static string GetEnvironmentVariable(string name)
        {
            return System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }

    
}
