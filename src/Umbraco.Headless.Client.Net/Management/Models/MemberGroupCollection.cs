using System.Collections.Generic;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class MemberGroupCollection
    {
        [JsonProperty("membergroups")]
        public IEnumerable<MemberGroup> Items { get; set; }
    }
}
