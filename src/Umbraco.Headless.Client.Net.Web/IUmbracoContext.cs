using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Web
{
    public interface IUmbracoContext
    {
        IContent? CurrentContent { get; set; }
    }

    internal class UmbracoContext : IUmbracoContext
    {
        public IContent? CurrentContent { get; set; }
    }
}
