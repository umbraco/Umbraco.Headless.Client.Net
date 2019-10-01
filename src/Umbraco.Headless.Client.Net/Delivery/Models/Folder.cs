using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public class Folder : ContentBase, IMedia
    {
        [JsonProperty("contentTypeAlias")]
        public string MediaTypeAlias { get; set; }
    }
}
