using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Samples.Web.Mvc
{
    public sealed class UmbracoCache
    {
        private readonly ContentDeliveryService _contentDelivery;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UmbracoCache(ContentDeliveryService contentDelivery, IHttpContextAccessor httpContextAccessor)
        {
            _contentDelivery = contentDelivery ?? throw new ArgumentNullException(nameof(contentDelivery));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        private HttpContext HttpContext => _httpContextAccessor.HttpContext;

        /// <summary>
        /// Gets an <see cref="IContent"/> object for a given URL and caches it for the current request
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<IContent> GetContentByUrl(string path)
        {
            var cacheKey = GetType().FullName;
            if (HttpContext.Items.TryGetValue(cacheKey, out var cache) == false)
            {
                HttpContext.Items[cacheKey] = cache = new Dictionary<string, IContent>();
            }

            var urlCache = (Dictionary<string, IContent>) cache;

            if (urlCache.TryGetValue(path, out var content) == false)
            {
                urlCache[path] = content = await _contentDelivery.Content.GetByUrl(path);
            }

            return content;
        }
    }
}
