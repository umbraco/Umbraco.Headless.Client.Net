using System.Collections.Generic;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class FormCollection
    {
        [JsonProperty("forms")]
        public IEnumerable<Form> Items { get; set; }
    }
}
