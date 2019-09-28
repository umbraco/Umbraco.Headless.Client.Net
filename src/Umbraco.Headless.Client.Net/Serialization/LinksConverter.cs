using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Umbraco.Headless.Client.Net.Delivery.Models;
using Umbraco.Headless.Client.Net.Delivery.Models.Hal;

namespace Umbraco.Headless.Client.Net.Serialization
{
    internal class LinksConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader is JsonTextReader)
            {
                var links = new List<Link>();
                var json = JObject.Load(reader);

                foreach (var link in json)
                {
                    var title = link.Key;
                    var href = string.Empty;
                    var templated = false;
                    if (link.Value.Count() == 1)
                    {
                        href = link.Value["href"].ToString();
                        links.Add(new Link
                        {
                            Title = title,
                            Href = href,
                            Templated = templated
                        });
                    }
                    else if (link.Value.Count() > 1)
                    {
                        if (link.Value.Type == JTokenType.Array)
                        {
                            var array = (JArray)link.Value;
                            foreach (var value in array)
                            {
                                href = value["href"].ToString();
                                templated = value["templated"]?.Value<bool>() ?? false;
                                links.Add(new Link
                                {
                                    Title = title,
                                    Href = href,
                                    Templated = templated
                                });
                            }
                        }
                        else
                        {
                            var value = (JObject)link.Value;
                            href = value["href"].ToString();
                            templated = value["templated"]?.Value<bool>() ?? false;
                            links.Add(new Link
                            {
                                Title = title,
                                Href = href,
                                Templated = templated
                            });
                        }
                    }
                }
                return new LinkCollection(links);
            }

            return new LinkCollection();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(ContentBase).IsAssignableFrom(objectType);
        }

        public override bool CanWrite => true;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var links = (List<Link>)value;
            writer.WriteStartObject();
            foreach (var link in links)
            {
                writer.WritePropertyName(link.Title);

                writer.WriteStartObject();
                writer.WritePropertyName("href");
                serializer.Serialize(writer, link.Href);
                if (link.Templated)
                {
                    writer.WritePropertyName("templated");
                    serializer.Serialize(writer, link.Templated);
                }
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }
    }
}
