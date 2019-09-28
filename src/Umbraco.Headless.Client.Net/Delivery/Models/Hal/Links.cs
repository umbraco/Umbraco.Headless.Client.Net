using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Delivery.Models.Hal
{
    internal class Links
    {
        [JsonProperty("self")]
        public Self Self { get; set; }

        [JsonProperty("next")]
        public Next Next { get; set; }

        [JsonProperty("page")]
        public Page Page { get; set; }
    }
}