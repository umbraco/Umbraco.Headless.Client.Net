using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class RootMemberTypeCollection
    {
        [JsonProperty("_embedded")]
        public MemberTypeCollection MemberTypes { get; set; }
    }
}
