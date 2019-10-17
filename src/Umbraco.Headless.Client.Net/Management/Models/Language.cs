using System;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class Language
    {
        public string CultureName { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string FallbackLanguage { get; set; }

        [JsonProperty("_id")]
        public Guid Id { get; set; }

        public bool IsDefault { get; set; }
        public bool IsMandatory { get; set; }
        public string IsoCode { get; set; }
    }
}
