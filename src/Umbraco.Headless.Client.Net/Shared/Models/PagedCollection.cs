using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Shared.Models
{
    public abstract class PagedCollection<T>
    {
        [JsonProperty("totalResults")]
        public int TotalResults { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("_links")]
        internal Links Links { get; set; }
    }
}
