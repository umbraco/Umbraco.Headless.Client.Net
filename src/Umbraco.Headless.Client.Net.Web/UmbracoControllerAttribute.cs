using System;

namespace Umbraco.Headless.Client.Net.Web
{
    /// <summary>
    /// Indicates that the associated component should match the specified Content Type alias.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class UmbracoControllerAttribute : Attribute
    {
        /// <summary>
        /// Creates a new instance of <see cref="UmbracoControllerAttribute"/>.
        /// </summary>
        /// <param name="contentTypeAlias">An optional Content Type alias this component is for.</param>
        public UmbracoControllerAttribute(string? contentTypeAlias = null)
        {
            ContentTypeAlias = contentTypeAlias;
        }

        /// <summary>
        /// The Content Type alias this component is for.
        /// If null the controller name will be used.
        /// </summary>
        public string? ContentTypeAlias { get; set; }
    }
}
