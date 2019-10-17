using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    internal class RootContent<T> where T : IContent
    {
        [JsonProperty("_embedded")]
        public ContentCollection<T> Content { get; set; }
    }
}
