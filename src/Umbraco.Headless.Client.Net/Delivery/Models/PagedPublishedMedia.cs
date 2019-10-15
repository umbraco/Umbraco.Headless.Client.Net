using Newtonsoft.Json;
using Umbraco.Headless.Client.Net.Shared.Models;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public class PagedPublishedMedia<T> : PagedCollection<T> where T : IPublishedMedia
    {
        [JsonProperty("_embedded")]
        public PublishedMediaCollection<T> Media { get; set; }
    }

    public class PagedPublishedMedia : PagedPublishedMedia<PublishedMedia>
    {
    }
}
