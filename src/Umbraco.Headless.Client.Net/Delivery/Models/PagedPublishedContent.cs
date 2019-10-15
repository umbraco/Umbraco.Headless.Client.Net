using Newtonsoft.Json;
using Umbraco.Headless.Client.Net.Shared.Models;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    [JsonObject]
    public class PagedPublishedContent<T> : PagedCollection<T> where T : IPublishedContent
    {
        [JsonProperty("_embedded")]
        public PublishedContentCollection<T> Content { get; set; }
    }

    [JsonObject]
    public class PagedPublishedContent : PagedPublishedContent<PublishedContent>
    {
    }
}
