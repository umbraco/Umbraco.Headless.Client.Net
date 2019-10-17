using System.Collections.Generic;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class MemberTypeCollection
    {
        [JsonProperty("membertypes")]
        public IEnumerable<MemberType> Items { get; set; }
    }
}
