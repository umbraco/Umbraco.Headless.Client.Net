using System;

namespace Umbraco.Headless.Client.Net.Delivery.Models
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class MediaModelAttribute : Attribute
    {
        public MediaModelAttribute(string mediaTypeAlias)
        {
            MediaTypeAlias = mediaTypeAlias;
        }

        public string MediaTypeAlias { get; }
    }
}
