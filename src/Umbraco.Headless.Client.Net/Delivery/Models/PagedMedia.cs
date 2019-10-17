using Newtonsoft.Json;
using Umbraco.Headless.Client.Net.Shared.Models;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public class PagedMedia<T> : PagedCollection<T> where T : IMedia
    {
        [JsonProperty("_embedded")]
        public MediaCollection<T> Media { get; set; }
    }

    public class PagedMedia : PagedMedia<Media>
    {
    }
}
