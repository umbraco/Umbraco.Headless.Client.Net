using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public class Folder : PublishedContentBase, IPublishedMedia
    {
        [JsonProperty("mediaTypeAlias")]
        public string MediaTypeAlias { get; set; }
    }
}
