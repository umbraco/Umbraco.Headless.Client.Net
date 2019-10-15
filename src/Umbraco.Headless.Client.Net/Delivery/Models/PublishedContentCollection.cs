using System.Collections.Generic;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public class PublishedContentCollection<T> where T : IPublishedContent
    {
        [JsonProperty("content")]
        public IEnumerable<T> Items { get; set; }
    }

    public class PublishedContentCollection : PublishedContentCollection<PublishedContent>
    {
    }
}
