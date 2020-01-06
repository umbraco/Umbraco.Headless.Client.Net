using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    /// <summary>
    /// Default model for "File" Media Types, which uses the Upload Property Editor for the "umbracoFile" Property.
    /// </summary>
    [MediaModel("File")]
    public class File : Media
    {
        [JsonProperty("umbracoFile")]
        public string FilePath { get; set; }

        [JsonProperty("umbracoBytes")]
        public long Bytes { get; set; }

        [JsonProperty("umbracoExtension")]
        public string Extension { get; set; }
    }
}
