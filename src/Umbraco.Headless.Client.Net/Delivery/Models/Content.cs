using System.Collections.Generic;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public class Content : ContentBase, IContent
    {
        public Content()
        {
            Properties = new Dictionary<string, object>();
        }

        [JsonProperty("contentTypeAlias")]
        public string ContentTypeAlias { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> Properties { get; set; }

        [JsonPropertyAttribute("_urls")]
        public IDictionary<string, string> Urls { get; set; }
    }
}
