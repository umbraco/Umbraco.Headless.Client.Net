using System.Collections.Generic;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public class MediaCollection<T> where T : IMedia
    {
        [JsonProperty("media")]
        public IEnumerable<T> Items { get; set; }
    }

    public class MediaCollection : MediaCollection<Media>
    {
    }
}
