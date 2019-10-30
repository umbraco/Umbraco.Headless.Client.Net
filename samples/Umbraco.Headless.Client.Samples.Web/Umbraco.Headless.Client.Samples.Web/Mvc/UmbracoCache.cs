using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Samples.Web.Mvc
{
    public sealed class UmbracoCache
    {
        private readonly ContentDeliveryService _contentDelivery;

        public UmbracoCache(ContentDeliveryService contentDelivery)
        {
            _contentDelivery = contentDelivery ?? throw new ArgumentNullException(nameof(contentDelivery));
        }

        private static readonly ConcurrentDictionary<string, Task<IContent>> UrlCache =
            new ConcurrentDictionary<string, Task<IContent>>();

        /// <summary>
        /// Gets an <see cref="IContent"/> object for a given URL and caches it
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<IContent> GetContentByUrl(string path) =>
            await UrlCache.GetOrAdd(path, async s => await _contentDelivery.Content.GetByUrl(path));

        /// <summary>
        /// Clears the cache
        /// </summary>
        public void ClearCache() => UrlCache.Clear();
    }
}
