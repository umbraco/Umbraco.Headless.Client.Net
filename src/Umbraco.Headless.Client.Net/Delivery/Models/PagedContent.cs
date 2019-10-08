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
    public class PagedContent
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
        public ContentCollection Content { get; set; }
    }
}
