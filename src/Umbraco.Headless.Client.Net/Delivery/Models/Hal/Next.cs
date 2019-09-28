using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models.Hal
{
    internal class Next
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}