using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Samples.Web.Mvc
{
  public class UmbracoContext
    {
        public UmbracoContext(UmbracoCache cache)
        {
            Cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public UmbracoCache Cache { get; }
        public IContent Content { get; private set; }

        internal async Task<bool> RouteUmbracoContentAsync(HttpContext context)
        {
            var url = context?.Request?.Path.Value;
            if (url == null)
                throw new InvalidOperationException("Could not determine the current URL path in the request");

            var content = await Cache.GetContentByUrl(url);
            Content = content;
            return content != null;
        }
    }
}
