using Newtonsoft.Json;
using Umbraco.Headless.Client.Net.Shared.Models;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    internal class RootContent<T> : PagedCollection<T> where T : IContent
    {
        [JsonProperty("_embedded")]
        public ContentCollection<T> Content { get; set; }
    }
}
