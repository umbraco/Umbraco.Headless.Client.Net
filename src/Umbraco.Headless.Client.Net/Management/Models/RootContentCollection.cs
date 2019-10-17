using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class RootContentCollection
    {
        [JsonProperty("_embedded")]
        public ContentCollection Content { get; set; }
    }
}
