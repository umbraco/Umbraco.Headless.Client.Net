using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class RootMemberGroupCollection
    {
        [JsonProperty("_embedded")]
        public MemberGroupCollection MemberGroups { get; set; }
    }
}
