using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class RootMediaCollection
    {
        [JsonProperty("_embedded")]
        public MediaCollection Media { get; set; }
    }
}
