using System.Collections.Generic;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public class Media : ContentBase, IMedia
    {
        public Media()
        {
            Properties = new Dictionary<string, object>();
        }

        [JsonProperty("mediaTypeAlias")]
        public string MediaTypeAlias { get; set; }


        [JsonExtensionData]
        public IDictionary<string, object> Properties { get; set; }
    }
}
