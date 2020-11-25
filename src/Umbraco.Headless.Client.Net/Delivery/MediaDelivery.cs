using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Delivery
{
    internal class MediaDelivery : IMediaDelivery
    {
        private readonly IHeadlessConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ModelNameResolver _modelNameResolver;
        private MediaDeliveryEndpoints _service;

        public MediaDelivery(IHeadlessConfiguration configuration, HttpClient httpClient, ModelNameResolver modelNameResolver)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _modelNameResolver = modelNameResolver;
        }

        private MediaDeliveryEndpoints Service =>
            _service
            ?? (_service = RestService.For<MediaDeliveryEndpoints>(_httpClient,
                new RefitSettings
                {
                    ContentSerializer = new JsonContentSerializer(new JsonSerializerSettings
                    {
                        Converters = _configuration.GetJsonConverters(_modelNameResolver)
                    })
                }));

        public async Task<IEnumerable<Media>> GetRoot()
        {
            var root = await Service.GetRoot(_configuration.ProjectAlias).ConfigureAwait(false);
            return root.Media.Items;
        }

        public async Task<IEnumerable<T>> GetRoot<T>() where T : IMedia
        {
            var service = RestService.For<TypedMediaDeliveryEndpoints<T>>(_httpClient);
            var root = await service.GetRoot(_configuration.ProjectAlias).ConfigureAwait(false);
            return root.Media.Items;
        }

        public async Task<Media> GetById(Guid id)
        {
            var content = await Service.GetById(_configuration.ProjectAlias, id).ConfigureAwait(false);
            return content;
        }

        public async Task<T> GetById<T>(Guid id) where T : IMedia
        {
            var service = RestService.For<TypedMediaDeliveryEndpoints<T>>(_httpClient);
            var content = await service.GetById(_configuration.ProjectAlias, id).ConfigureAwait(false);
            return content;
        }

        public async Task<PagedMedia> GetChildren(Guid id, int page = 0, int pageSize = 10)
        {
            var content = await Service.GetChildren(_configuration.ProjectAlias, id, page, pageSize).ConfigureAwait(false);
            return content;
        }

        public async Task<PagedMedia<T>> GetChildren<T>(Guid id, int page = 0, int pageSize = 10) where T : IMedia
        {
            var service = RestService.For<TypedMediaDeliveryEndpoints<T>>(_httpClient);
            var content = await service.GetChildren(_configuration.ProjectAlias, id, page, pageSize).ConfigureAwait(false);
            return content;
        }
    }
}
