using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    internal class RootMedia<T> where T : IMedia
    {
        [JsonProperty("_embedded")]
        public MediaCollection<T> Media { get; set; }
    }
}
