using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Refit;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class Media : Entity
    {
        // we don't want links to show up in the properties dictionary
        // and we don't really need them so we just map them to this field
        [JsonProperty("_links", NullValueHandling = NullValueHandling.Ignore)]
        private object _links;

        [JsonProperty("_hasChildren", NullValueHandling = NullValueHandling.Ignore)]
        private bool? _hasChildren;

        [JsonProperty("_level", NullValueHandling = NullValueHandling.Ignore)]
        private int? _level;

        public Media()
        {
            Properties = new Dictionary<string, object>();
            Files = new Dictionary<string, MultipartItem>();
        }

        public string MediaTypeAlias { get; set; }

        [JsonIgnore]
        public bool HasChildren => _hasChildren.GetValueOrDefault();

        [JsonIgnore]
        public int Level => _level.GetValueOrDefault();

        public string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid? ParentId { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> Properties { get; }

        [JsonIgnore]
        internal IDictionary<string, MultipartItem> Files { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? SortOrder { get; set; }

        public object GetValue(string alias, object defaultValue = null)
        {
            if (Properties.TryGetValue(alias, out var value))
            {
                return value;
            }

            return defaultValue;
        }

        public void SetValue(string alias, object value)
        {
            Properties[alias] = value;
        }

        public void SetValue(string alias, object value, MultipartItem file)
        {
            SetValue(alias, value);
            Files[alias] = file;
        }
    }
}
