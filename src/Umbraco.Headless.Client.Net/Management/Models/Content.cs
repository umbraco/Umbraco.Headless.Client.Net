using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Umbraco.Headless.Client.Net.Management.Models
{
    public class Content : Entity
    {
        [JsonExtensionData] private IDictionary<string, JToken> _additionalData;

        // we don't want links to show up in the properties dictionary
        // and we don't really need them so we just map them to this field
        [JsonProperty("_links", NullValueHandling = NullValueHandling.Ignore)]
        private object _links;

        [JsonProperty("_hasChildren", NullValueHandling = NullValueHandling.Ignore)]
        private bool? _hasChildren;

        [JsonProperty("_level", NullValueHandling = NullValueHandling.Ignore)]
        private int? _level;

        public Content()
        {
            Name = new Dictionary<string, string>();
            Properties = new Dictionary<string, IDictionary<string, object>>();
        }

        public string ContentTypeAlias { get; set; }

        [JsonProperty("_currentVersionState", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, ContentSavedState> CurrentVersionState { get; private set; }

        [JsonIgnore]
        public bool HasChildren => _hasChildren.GetValueOrDefault();

        [JsonIgnore]
        public int Level => _level.GetValueOrDefault();

        public IDictionary<string, string> Name { get; private set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid? ParentId { get; set; }

        [JsonIgnore]
        public IDictionary<string, IDictionary<string, object>> Properties { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? SortOrder { get; set; }

        [JsonProperty("_updateDate", NullValueHandling = NullValueHandling.Ignore)]
        public new IDictionary<string, DateTime?> UpdateDate { get; private set; }

        public object GetValue(string alias, string culture = null, object defaultValue = null)
        {
            if (culture == null)
                culture = "$invariant";

            if (Properties.TryGetValue(alias, out var cultures) && cultures != null &&
                cultures.TryGetValue(culture, out var value))
            {
                return value;
            }

            return defaultValue;
        }

        public void SetValue(string alias, object value, string culture = null)
        {
            if (culture == null)
                culture = "$invariant";

            if (Properties.TryGetValue(alias, out var cultures) == false)
            {
                Properties[alias] = cultures = new Dictionary<string, object>();
            }

            cultures[culture] = value;
        }

        [OnSerializing]
        internal void OnSerializingMethod(StreamingContext context)
        {
            _additionalData = new Dictionary<string, JToken>();
            foreach (var pair in Properties)
            {
                var fromObject = JToken.FromObject(pair.Value);
                _additionalData.Add(pair.Key, fromObject);
            }
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Properties = new Dictionary<string, IDictionary<string, object>>();

            if (_additionalData == null)
                return;

            foreach (var pair in _additionalData)
            {
                try
                {
                    Properties.Add(pair.Key, pair.Value.ToObject<IDictionary<string, object>>());
                }
                catch (JsonSerializationException)
                {
                    // swallow exception
                }
            }

            _additionalData.Clear();
        }
    }
}
