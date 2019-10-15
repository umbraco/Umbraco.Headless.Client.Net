using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    internal class RootPublishedContent<T> where T : IPublishedContent
    {
        [JsonProperty("_embedded")]
        public PublishedContentCollection<T> Content { get; set; }
    }
}
