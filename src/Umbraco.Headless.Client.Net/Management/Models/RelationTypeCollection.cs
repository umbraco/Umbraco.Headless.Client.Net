using System.Collections.Generic;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class RelationTypeCollection
    {
        [JsonProperty("relationtypes")]
        public IEnumerable<RelationType> Items { get; set; }
    }
}