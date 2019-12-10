using System;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;

namespace Umbraco.Headless.Client.Samples.Web.Serialization
{
    public class HtmlContentConverter : JsonConverter
    {
        public override bool CanWrite { get; } = false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = serializer.Deserialize<string>(reader);
            return new HtmlString(value);
        }

        public override bool CanConvert(Type objectType) => typeof(IHtmlContent).IsAssignableFrom(objectType);

    }
}
