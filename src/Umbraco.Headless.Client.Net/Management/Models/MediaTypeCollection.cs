using System.Collections.Generic;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class MediaTypeCollection
    {
        [JsonProperty("mediatypes")]
        public IEnumerable<MediaType> Items { get; set; }
    }
}
