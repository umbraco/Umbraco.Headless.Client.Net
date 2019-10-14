using System;
using Newtonsoft.Json;
using Umbraco.Headless.Client.Net.Shared.Models;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class RelationType
    {
        public string Alias { get; set; }
        public UmbracoObjectTypes ChildObjectType { get; set; }

        [JsonProperty("_id")]
        public Guid Id { get; set; }

        public bool IsBidirectional { get; set; }
        public string Name { get; set; }
        public UmbracoObjectTypes ParentObjectType { get; set; }
    }
}
