using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class RootRelationCollection
    {
        [JsonProperty("_embedded")]
        public RelationCollection Relations { get; set; }
    }
}
