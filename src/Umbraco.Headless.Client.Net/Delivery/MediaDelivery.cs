using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Delivery
{
    internal class MediaDelivery : IMediaDelivery
    {
        private readonly HttpClient _httpClient;
        public MediaDelivery(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Media>> GetRoot()
        {
            var service = RestService.For<MediaDeliveryEndpoints>(_httpClient);
            var root = await service.GetRoot();
            return root.Media.Items;
        }

        public async Task<IEnumerable<T>> GetRoot<T>() where T : IContentBase
        {
            var service = RestService.For<TypedMediaDeliveryEndpoints<T>>(_httpClient);
            var root = await service.GetRoot();
            return root.Media.Items;
        }

        public async Task<Media> GetById(Guid id)
        {
            var service = RestService.For<MediaDeliveryEndpoints>(_httpClient);
            var content = await service.GetById(id);
            return content;
        }

        public async Task<T> GetById<T>(Guid id) where T : IContentBase
        {
            var service = RestService.For<TypedMediaDeliveryEndpoints<T>>(_httpClient);
            var content = await service.GetById(id);
            return content;
        }

        public async Task<PagedMedia> GetChildren(Guid id, int page = 0, int pageSize = 10)
        {
            var service = RestService.For<MediaDeliveryEndpoints>(_httpClient);
            var content = await service.GetChildren(id, page, pageSize);
            return content;
        }

        public async Task<PagedMedia<T>> GetChildren<T>(Guid id, int page = 0, int pageSize = 10) where T : IContentBase
        {
            var service = RestService.For<TypedMediaDeliveryEndpoints<T>>(_httpClient);
            var content = await service.GetChildren(id, page, pageSize);
            return content;
        }
    }
}
