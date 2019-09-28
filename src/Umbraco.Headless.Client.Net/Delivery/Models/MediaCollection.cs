using System.Collections.Generic;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public class MediaCollection<T> where T : IContentBase
    {
        [JsonProperty("media")]
        public IEnumerable<T> Items { get; set; }
    }

    public class MediaCollection
    {
        [JsonProperty("media")]
        public IEnumerable<Media> Items { get; set; }
    }
}
