using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public class MediaWithCrops : ImageCropper
    {
        [JsonProperty("media")]
        public Media Media { get; set; }
    }
}
