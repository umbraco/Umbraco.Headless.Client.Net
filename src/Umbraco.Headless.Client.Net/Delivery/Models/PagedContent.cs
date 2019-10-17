using Newtonsoft.Json;
using Umbraco.Headless.Client.Net.Shared.Models;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    [JsonObject]
    public class PagedContent<T> : PagedCollection<T> where T : IContent
    {
        [JsonProperty("_embedded")]
        public ContentCollection<T> Content { get; set; }
    }

    [JsonObject]
    public class PagedContent : PagedContent<Content>
    {
    }
}
