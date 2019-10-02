using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Delivery
{
    internal class ContentDelivery : IContentDelivery
    {
        private readonly IHeadlessConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ContentDelivery(IHeadlessConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Content>> GetRoot(string culture)
        {
            var service = RestService.For<ContentDeliveryEndpoints>(_httpClient);
            var root = await service.GetRoot(culture);
            return root.Content.Items;
        }

        public async Task<IEnumerable<T>> GetRoot<T>(string culture) where T : IContentBase
        {
            var service = RestService.For<TypedContentRootDeliveryEndpoints<T>>(_httpClient);
            var root = await service.GetRoot(culture);
            return root.Content.Items;
        }

        public async Task<Content> GetById(Guid id, string culture, int depth)
        {
            var service = RestService.For<ContentDeliveryEndpoints>(_httpClient);
            var content = await service.GetById(culture, id, depth);
            return content;
        }

        public async Task<T> GetById<T>(Guid id, string culture, int depth) where T : IContentBase
        {
            var service = RestService.For<TypedContentDeliveryEndpoints<T>>(_httpClient);
            var content = await service.GetById(culture, id, depth);
            return content;
        }

        public async Task<Content> GetByUrl(string url, string culture, int depth)
        {
            var service = RestService.For<ContentDeliveryEndpoints>(_httpClient);
            var content = await service.GetByUrl(culture, url, depth);
            return content;
        }

        public async Task<T> GetByUrl<T>(string url, string culture, int depth) where T : IContentBase
        {
            var service = RestService.For<TypedContentDeliveryEndpoints<T>>(_httpClient);
            var content = await service.GetByUrl(culture, url, depth);
            return content;
        }

        public async Task<PagedContent> GetChildren(Guid id, string culture, int page, int pageSize)
        {
            var service = RestService.For<ContentDeliveryEndpoints>(_httpClient);
            var content = await service.GetChildren(culture, id, page, pageSize);
            return content;
        }

        public async Task<PagedContent<T>> GetChildren<T>(Guid id, string culture, int page, int pageSize) where T : IContentBase
        {
            var service = RestService.For<TypedPagedContentDeliveryEndpoints<T>>(_httpClient);
            var content = await service.GetChildren(culture, id, page, pageSize);
            return content;
        }

        public async Task<PagedContent> GetDescendants(Guid id, string culture, int page, int pageSize)
        {
            var service = RestService.For<ContentDeliveryEndpoints>(_httpClient);
            var content = await service.GetDescendants(culture, id, page, pageSize);
            return content;
        }

        public async Task<PagedContent<T>> GetDescendants<T>(Guid id, string culture, int page, int pageSize) where T : IContentBase
        {
            var service = RestService.For<TypedPagedContentDeliveryEndpoints<T>>(_httpClient);
            var content = await service.GetDescendants(culture, id, page, pageSize);
            return content;
        }

        public async Task<IEnumerable<Content>> GetAncestors(Guid id, string culture)
        {
            var service = RestService.For<ContentDeliveryEndpoints>(_httpClient);
            var root = await service.GetAncestors(culture, id);
            return root.Content.Items;
        }

        public async Task<IEnumerable<T>> GetAncestors<T>(Guid id, string culture) where T : IContentBase
        {
            var service = RestService.For<TypedContentRootDeliveryEndpoints<T>>(_httpClient);
            var root = await service.GetAncestors(culture, id);
            return root.Content.Items;
        }
    }
}
