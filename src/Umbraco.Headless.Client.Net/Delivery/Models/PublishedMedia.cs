using System.Collections.Generic;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public class PublishedMedia : PublishedContentBase, IPublishedMedia
    {
        public PublishedMedia()
        {
            Properties = new Dictionary<string, object>();
        }

        [JsonProperty("mediaTypeAlias")]
        public string MediaTypeAlias { get; set; }


        [JsonExtensionData]
        public IDictionary<string, object> Properties { get; set; }
    }
}
