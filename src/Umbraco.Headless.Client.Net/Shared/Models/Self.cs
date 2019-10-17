using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Shared.Models
{
    internal class Self
    {
        [JsonProperty("href")]
        public string Href { get; set; }
    }
}
