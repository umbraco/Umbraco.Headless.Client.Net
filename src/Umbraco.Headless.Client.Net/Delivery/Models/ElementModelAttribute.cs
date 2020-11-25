using System;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ElementModelAttribute : Attribute
    {
        public ElementModelAttribute(string contentTypeAlias)
        {
            ContentTypeAlias = contentTypeAlias;
        }

        public string ContentTypeAlias { get; }
    }
}
