using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    internal class RootPublishedMedia<T> where T : IPublishedMedia
    {
        [JsonProperty("_embedded")]
        public PublishedMediaCollection<T> Media { get; set; }
    }
}
