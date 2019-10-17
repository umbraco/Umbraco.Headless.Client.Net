using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class RootDocumentTypeCollection
    {
        [JsonProperty("_embedded")]
        public DocumentTypeCollection DocumentTypes { get; set; }
    }
}
