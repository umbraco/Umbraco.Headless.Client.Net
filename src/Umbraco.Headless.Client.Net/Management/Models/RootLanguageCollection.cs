using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class RootLanguageCollection
    {
        [JsonProperty("_embedded")]
        public LanguageCollection Languages { get; set; }
    }
}
