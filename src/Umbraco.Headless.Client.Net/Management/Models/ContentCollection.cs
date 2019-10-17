using System.Collections.Generic;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class ContentCollection
    {
        [JsonProperty("content")]
        public IEnumerable<Content> Items { get; set; }
     }
}
