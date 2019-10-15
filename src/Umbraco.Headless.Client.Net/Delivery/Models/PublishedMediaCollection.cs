using System.Collections.Generic;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public class PublishedMediaCollection<T> where T : IPublishedMedia
    {
        [JsonProperty("media")]
        public IEnumerable<T> Items { get; set; }
    }

    public class MediaCollection : PublishedMediaCollection<PublishedMedia>
    {
    }
}
