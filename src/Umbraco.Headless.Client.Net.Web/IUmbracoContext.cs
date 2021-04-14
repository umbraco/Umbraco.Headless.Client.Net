using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Web
{
    public interface IUmbracoContext
    {
        /// <summary>
        /// Get or set the content associated with the current request.
        /// </summary>
        IContent? CurrentContent { get; set; }

        /// <summary>
        /// Get or set if the current request is in preview mode,
        /// </summary>
        bool IsInPreviewMode { get; set; }
    }

    internal class UmbracoContext : IUmbracoContext
    {
        public IContent? CurrentContent { get; set; }
        public bool IsInPreviewMode { get; set; }
    }
}
