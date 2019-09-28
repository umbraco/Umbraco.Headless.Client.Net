using System.Collections.Generic;
using Newtonsoft.Json;
using Umbraco.Headless.Client.Net.Serialization;

namespace Umbraco.Headless.Client.Net.Delivery.Models.Hal
{
    [JsonConverter(typeof(LinksConverter))]
    public class LinkCollection : List<Link>
    {
        public LinkCollection() { }

        public LinkCollection(List<Link> links) : base((IEnumerable<Link>)links) { }
    }
}