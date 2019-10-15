using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Shared.Models
{
    public abstract class PagedCollection<T>
    {
        [JsonProperty("_totalItems")]
        public int TotalItems { get; set; }

        [JsonProperty("_totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty("_page")]
        public int Page { get; set; }

        [JsonProperty("_pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("_links")]
        internal Links Links { get; set; }
    }
}
