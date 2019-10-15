using System;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class Relation
    {
        public Guid ChildId { get; set; }
        public string Comment { get; set; }

        [JsonProperty("_id")]
        public int Id { get; set; }

        public Guid ParentId { get; set; }
        public string RelationTypeAlias { get; set; }
    }
}
