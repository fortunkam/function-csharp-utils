using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using Memoryleek.FunctionCSharpUtils.Models;

namespace Memoryleek.FunctionCSharpUtils
{

    
    ///////////////////////////////////////////////
    //
    // Get the outbound IP used by this function
    //
    ///////////////////////////////////////////////
    public static class GetOutboundIp
    {
        [FunctionName("GetOutboundIp")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Calling HttpBin.org to get current IP");

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://httpbin.org/ip");
            dynamic content = await response.Content.ReadAsAsync<dynamic>();

            return new OkObjectResult(new GetOutboundIpResponse
            {
                IpAddress = content.origin
            });
        }
    }
}
