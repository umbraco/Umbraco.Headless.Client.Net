using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models.Hal
{
    internal class RootContent<T> where T : IContentBase
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
        public Links Links { get; set; }

        [JsonProperty("_embedded")]
        public ContentCollection<T> Content { get; set; }
    }
}
