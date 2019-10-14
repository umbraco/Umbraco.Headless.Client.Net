using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class MemberGroup : Entity
    {
        [JsonProperty("_name")]
        public string Name { get; set; }
    }
}
