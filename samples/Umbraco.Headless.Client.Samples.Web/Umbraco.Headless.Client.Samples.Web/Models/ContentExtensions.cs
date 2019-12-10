using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json.Linq;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Samples.Web.Models
{
    public static class ContentExtensions
    {
        public static object Value(this Content content, string alias)
        {
            content.Properties.TryGetValue(alias, out var value);
            return value;
        }

        public static T Value<T>(this Content content, string alias)
        {
            var value = Value(content, alias);
            if (value == null) return default;

            var destriptor = TypeDescriptor.GetConverter(typeof(T));
            if (destriptor.CanConvertFrom(value.GetType()))
                return (T) destriptor.ConvertFrom(value);

            if (value is string strValue && typeof(T) == typeof(IHtmlContent))
                return (T) (object) new HtmlString(strValue);

            if (value is JToken token)
                return token.ToObject<T>();

            return (T) Convert.ChangeType(value, typeof(T));
        }

        public static bool IsVisible(this IContent content)
        {
            if (content is IHideInNavigation hideInNavigation)
                return hideInNavigation.HideInNavigation == false;

            if (content is Content c)
                return c.Value<bool>("umbracoNaviHide") == false;

            return true;
        }
    }
    public static class ElementExtensions
    {
        public static object Value(this Element content, string alias)
        {
            content.Properties.TryGetValue(alias, out var value);
            return value;
        }

        public static T Value<T>(this Element content, string alias)
        {
            var value = Value(content, alias);
            if (value == null) return default;

            var destriptor = TypeDescriptor.GetConverter(typeof(T));
            if (destriptor.CanConvertFrom(value.GetType()))
                return (T) destriptor.ConvertFrom(value);

            if (value is string strValue && typeof(T) == typeof(IHtmlContent))
                return (T) (object) new HtmlString(strValue);

            if (value is JToken token)
                return token.ToObject<T>();

            return (T) Convert.ChangeType(value, typeof(T));
        }
    }
}
