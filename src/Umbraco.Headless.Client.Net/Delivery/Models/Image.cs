using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    /// <summary>
    /// Default model for "Image" Media Types, which uses the ImageCropper for the "umbracoFile" Property.
    /// </summary>
    [MediaModel("Image")]
    public class Image : Media
    {
        [JsonProperty("umbracoFile")]
        public ImageCropper File { get; set; }

        [JsonProperty("umbracoWidth")]
        public int Width { get; set; }

        [JsonProperty("umbracoHeight")]
        public int Height { get; set; }

        [JsonProperty("umbracoBytes")]
        public long Bytes { get; set; }

        [JsonProperty("umbracoExtension")]
        public string Extension { get; set; }
   }
}
