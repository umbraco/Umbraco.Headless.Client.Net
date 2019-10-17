using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Shared.Models
{
    internal class Next
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}
