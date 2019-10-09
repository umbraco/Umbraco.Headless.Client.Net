using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public abstract class ContentType<TPropertyInfoType> : Entity where TPropertyInfoType : ContentTypePropertyInfo
    {
        public string Alias { get; set; }
        public IEnumerable<string> Compositions { get; set; }
        public string Description { get; set; }
        public IEnumerable<ContentTypePropertyGroupInfo<TPropertyInfoType>> Groups { get; set; }
        public string Name { get; set; }

        [JsonProperty("_updateDate")]
        public DateTime UpdateDate { get; set; }
    }
}
