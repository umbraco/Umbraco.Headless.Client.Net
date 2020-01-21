using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    public class ContentFilter
    {
        public ContentFilter(ContentFilterProperties[] properties)
        {
            Properties = properties;
        }

        public ContentFilter(string contentTypeAlias, ContentFilterProperties[] properties)
        {
            ContentTypeAlias = contentTypeAlias;
            Properties = properties;
        }

        public string ContentTypeAlias { get; internal set; }
        public ContentFilterProperties[] Properties { get; }

    }

    public class ContentFilterProperties
    {
        public ContentFilterProperties(string @alias, string value, ContentFilterMatch match)
        {
            Alias = alias;
            Value = value;
            Match = match;
        }

        public string Alias { get; }
        public string Value { get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ContentFilterMatch Match { get; }
    }

    public enum ContentFilterMatch
    {
        Contains,
        Like
    }
}
