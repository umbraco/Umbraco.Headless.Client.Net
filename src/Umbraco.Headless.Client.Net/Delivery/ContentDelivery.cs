using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Delivery
{
    internal class ContentDelivery : IContentDelivery
    {
        private readonly HttpClient _httpClient;

        public ContentDelivery(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Content>> GetRoot()
        {
            var service = RestService.For<ContentDeliveryEndpoints>(_httpClient);
            var root = await service.GetRoot();
            return root.Content.Items;
        }

        public async Task<IEnumerable<T>> GetRoot<T>() where T : IContentBase
        {
            var service = RestService.For<TypedContentRootDeliveryEndpoints<T>>(_httpClient);
            var root = await service.GetRoot();
            return root.Content.Items;
        }

        public async Task<Content> GetById(Guid id, int depth = 1)
        {
            var service = RestService.For<ContentDeliveryEndpoints>(_httpClient);
            var content = await service.GetById(id, depth);
            return content;
        }

        public async Task<T> GetById<T>(Guid id, int depth = 1) where T : IContentBase
        {
            var service = RestService.For<TypedContentDeliveryEndpoints<T>>(_httpClient);
            var content = await service.GetById(id, depth);
            return content;
        }

        public async Task<Content> GetByUrl(string url, int depth = 1)
        {
            var service = RestService.For<ContentDeliveryEndpoints>(_httpClient);
            var content = await service.GetByUrl(url, depth);
            return content;
        }

        public async Task<T> GetByUrl<T>(string url, int depth = 1) where T : IContentBase
        {
            var service = RestService.For<TypedContentDeliveryEndpoints<T>>(_httpClient);
            var content = await service.GetByUrl(url, depth);
            return content;
        }

        public async Task<PagedContent> GetChildren(Guid id, int page = 1, int pageSize = 10)
        {
            var service = RestService.For<ContentDeliveryEndpoints>(_httpClient);
            var content = await service.GetChildren(id, page, pageSize);
            return content;
        }

        public async Task<PagedContent<T>> GetChildren<T>(Guid id, int page = 1, int pageSize = 10) where T : IContentBase
        {
            var service = RestService.For<TypedPagedContentDeliveryEndpoints<T>>(_httpClient);
            var content = await service.GetChildren(id, page, pageSize);
            return content;
        }

        public async Task<PagedContent> GetDescendants(Guid id, int page = 1, int pageSize = 10)
        {
            var service = RestService.For<ContentDeliveryEndpoints>(_httpClient);
            var content = await service.GetDescendants(id, page, pageSize);
            return content;
        }

        public async Task<PagedContent<T>> GetDescendants<T>(Guid id, int page = 1, int pageSize = 10) where T : IContentBase
        {
            var service = RestService.For<TypedPagedContentDeliveryEndpoints<T>>(_httpClient);
            var content = await service.GetDescendants(id, page, pageSize);
            return content;
        }

        public async Task<IEnumerable<Content>> GetAncestors(Guid id)
        {
            var service = RestService.For<ContentDeliveryEndpoints>(_httpClient);
            var root = await service.GetAncestors(id);
            return root.Content.Items;
        }

        public async Task<IEnumerable<T>> GetAncestors<T>(Guid id) where T : IContentBase
        {
            var service = RestService.For<TypedContentRootDeliveryEndpoints<T>>(_httpClient);
            var root = await service.GetAncestors(id);
            return root.Content.Items;
        }
    }
}
