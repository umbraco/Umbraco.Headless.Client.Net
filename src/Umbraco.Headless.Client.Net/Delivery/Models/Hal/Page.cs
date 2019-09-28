using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models.Hal
{
    internal class Page
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }
    }
}