using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Serialization
{
    internal class ModelConverter<TInterface, TImplementation> : JsonConverter
    {
        private readonly IDictionary<string, Type> _types;
        private readonly string _aliasProperty;

        public ModelConverter(IDictionary<string, Type> types, string aliasProperty)
        {
            _types = types ?? throw new ArgumentNullException(nameof(types));
            _aliasProperty = aliasProperty ?? throw new ArgumentNullException(nameof(aliasProperty));
        }

        public override bool CanWrite { get; } = false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            throw new NotImplementedException();

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = serializer.Deserialize<JObject>(reader);
            var contentTypeAlias = obj.GetValue(_aliasProperty).Value<string>();

            return _types.TryGetValue(contentTypeAlias, out var type) ?
                obj.ToObject(type, serializer) : obj.ToObject<TImplementation>();
        }

        public override bool CanConvert(Type objectType) =>
            typeof(TInterface) == objectType || typeof(TImplementation) == objectType;
    }
}
