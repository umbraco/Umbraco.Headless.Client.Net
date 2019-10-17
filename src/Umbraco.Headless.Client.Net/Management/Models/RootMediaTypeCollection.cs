using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class RootMediaTypeCollection
    {
        [JsonProperty("_embedded")]
        public MediaTypeCollection MediaTypes { get; set; }
    }
}
