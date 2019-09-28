using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models.Hal
{
    public class Link
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }
    }
}
