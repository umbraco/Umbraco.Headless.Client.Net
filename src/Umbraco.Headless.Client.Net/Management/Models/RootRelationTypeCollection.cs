using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class RootRelationTypeCollection
    {
        [JsonProperty("_embedded")]
        public RelationTypeCollection RelationTypes { get; set; }
    }
}