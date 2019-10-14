using System.Collections.Generic;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class LanguageCollection
    {
        [JsonProperty("languages")]
        public IEnumerable<Language> Items { get; set; }
    }
}
