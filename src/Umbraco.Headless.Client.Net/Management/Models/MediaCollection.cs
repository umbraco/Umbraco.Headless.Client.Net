using System.Collections.Generic;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class MediaCollection
    {
        [JsonProperty("media")]
        public IEnumerable<Media> Items { get; set; }
     }
}
