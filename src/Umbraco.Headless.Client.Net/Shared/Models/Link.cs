using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Shared.Models
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
