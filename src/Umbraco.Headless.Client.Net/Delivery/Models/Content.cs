using System.Collections.Generic;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public class Content : ContentBase, IContent
    {
        [JsonProperty("contentTypeAlias")]
        public string ContentTypeAlias { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();

        [JsonPropertyAttribute("_urls")]
        public IDictionary<string, string> Urls { get; set; }
    }
}
