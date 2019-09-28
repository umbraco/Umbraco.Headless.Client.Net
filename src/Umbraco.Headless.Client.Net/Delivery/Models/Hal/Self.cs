using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models.Hal
{
    internal class Self
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}