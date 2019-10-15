using System.Collections.Generic;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class RelationCollection
    {
        [JsonProperty("relations")]
        public IEnumerable<Relation> Items { get; set; }
    }
}
