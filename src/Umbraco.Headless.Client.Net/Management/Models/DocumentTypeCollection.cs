using System.Collections.Generic;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class DocumentTypeCollection
    {
        [JsonProperty("contenttypes")]
        public IEnumerable<DocumentType> Items { get; set; }
    }
}
