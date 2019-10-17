using Newtonsoft.Json;
using Umbraco.Headless.Client.Net.Shared.Models;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class PagedMedia : PagedCollection<Media>
    {
        [JsonProperty("_embedded")]
        public MediaCollection Media { get; set; }
    }
}
