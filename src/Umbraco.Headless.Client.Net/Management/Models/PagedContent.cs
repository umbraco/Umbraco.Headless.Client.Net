using Newtonsoft.Json;
using Umbraco.Headless.Client.Net.Shared.Models;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class PagedContent : PagedCollection<Content>
    {
        [JsonProperty("_embedded")]
        public ContentCollection Content { get; set; }
    }
}
