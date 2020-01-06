using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Serialization
{
    internal class MediaConverter : JsonConverter
    {
        private readonly IDictionary<string, Type> _types;

        public MediaConverter(IDictionary<string, Type> types)
        {
            _types = types ?? throw new ArgumentNullException(nameof(types));
        }

        public override bool CanWrite { get; } = false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            throw new NotImplementedException();

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = serializer.Deserialize<JObject>(reader);
            var mediaTypeAlias = obj.GetValue("mediaTypeAlias").Value<string>();

            return _types.TryGetValue(mediaTypeAlias, out var type) ? obj.ToObject(type) : obj.ToObject<Media>();
        }

        public override bool CanConvert(Type objectType) => typeof(IMedia).IsAssignableFrom(objectType);
    }
}
