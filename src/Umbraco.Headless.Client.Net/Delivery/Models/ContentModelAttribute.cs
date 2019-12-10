using System;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ContentModelAttribute : Attribute
    {
        public ContentModelAttribute(string contentTypeAlias)
        {
            ContentTypeAlias = contentTypeAlias;
        }

        public string ContentTypeAlias { get; }
    }
}
