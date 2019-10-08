using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Shared.Models
{
    internal class Page
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }
    }
}
