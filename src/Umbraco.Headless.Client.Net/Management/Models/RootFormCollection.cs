using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class RootFormCollection
    {
        [JsonProperty("_embedded")]
        public FormCollection Forms { get; set; }
    }
}
