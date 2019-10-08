using Newtonsoft.Json;
using Umbraco.Headless.Client.Net.Shared.Models;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public class PagedMedia<T> : PagedCollection<T> where T : IMedia
    {
        [JsonProperty("_embedded")]
        public MediaCollection<T> Media { get; set; }
    }

    public class PagedMedia
    {
        [JsonProperty("_totalItems")]
        public int TotalResults { get; set; }

        [JsonProperty("_totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty("_page")]
        public int Page { get; set; }

        [JsonProperty("_pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("_embedded")]
        public MediaCollection Media { get; set; }
    }
}
