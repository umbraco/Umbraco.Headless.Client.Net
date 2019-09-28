using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public class PagedMedia<T> where T : IContentBase
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
