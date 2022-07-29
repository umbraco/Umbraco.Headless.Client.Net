using System.Collections.Generic;
using Newtonsoft.Json;
using Umbraco.Headless.Client.Net.Shared.Models;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public class PagedRedirect : PagedCollection<PagedRedirect>
    {
        [JsonProperty("redirects")]
        public IDictionary<string, string[]> Redirects { get; set; }
    }

}

