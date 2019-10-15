using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Delivery
{
    internal class MediaDelivery : IMediaDelivery
    {
        private readonly IHeadlessConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public MediaDelivery(IHeadlessConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PublishedMedia>> GetRoot()
        {
            var service = RestService.For<MediaDeliveryEndpoints>(_httpClient);
            var root = await service.GetRoot(_configuration.ProjectAlias);
            return root.Media.Items;
        }

        public async Task<IEnumerable<T>> GetRoot<T>() where T : IPublishedMedia
        {
            var service = RestService.For<TypedMediaDeliveryEndpoints<T>>(_httpClient);
            var root = await service.GetRoot(_configuration.ProjectAlias);
            return root.Media.Items;
        }

        public async Task<PublishedMedia> GetById(Guid id)
        {
            var service = RestService.For<MediaDeliveryEndpoints>(_httpClient);
            var content = await service.GetById(_configuration.ProjectAlias, id);
            return content;
        }

        public async Task<T> GetById<T>(Guid id) where T : IPublishedMedia
        {
            var service = RestService.For<TypedMediaDeliveryEndpoints<T>>(_httpClient);
            var content = await service.GetById(_configuration.ProjectAlias, id);
            return content;
        }

        public async Task<PagedPublishedMedia> GetChildren(Guid id, int page = 0, int pageSize = 10)
        {
            var service = RestService.For<MediaDeliveryEndpoints>(_httpClient);
            var content = await service.GetChildren(_configuration.ProjectAlias, id, page, pageSize);
            return content;
        }

        public async Task<PagedPublishedMedia<T>> GetChildren<T>(Guid id, int page = 0, int pageSize = 10) where T : IPublishedMedia
        {
            var service = RestService.For<TypedMediaDeliveryEndpoints<T>>(_httpClient);
            var content = await service.GetChildren(_configuration.ProjectAlias, id, page, pageSize);
            return content;
        }
    }
}
