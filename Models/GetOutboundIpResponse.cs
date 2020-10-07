using Newtonsoft.Json;

namespace Memoryleek.FunctionCSharpUtils.Models
{
    public class GetOutboundIpResponse
    {
        [JsonProperty("ip")]
        public string IpAddress { get; set; }
    }
}