using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Delivery
{
    internal class MediaDelivery : DeliveryBase, IMediaDelivery
    {
        private readonly HttpClient _httpClient;
        private readonly RefitSettings _refitSettings;
        private MediaDeliveryEndpoints _service;

        public MediaDelivery(IHeadlessConfiguration configuration, HttpClient httpClient, RefitSettings refitSettings) : base(configuration)
        {
            _httpClient = httpClient;
            _refitSettings = refitSettings;
        }

        private MediaDeliveryEndpoints Service =>
            _service ?? (_service = RestService.For<MediaDeliveryEndpoints>(_httpClient, _refitSettings));

        public async Task<IEnumerable<Media>> GetRoot()
        {
            var result = await Get(x => x.GetRoot(Configuration.ProjectAlias)).ConfigureAwait(false);
            return result?.Media.Items;
        }

        public async Task<IEnumerable<T>> GetRoot<T>() where T : IMedia
        {
            var result = await GetTyped<T>(x => x.GetRoot(Configuration.ProjectAlias)).ConfigureAwait(false);
            return result?.Media.Items;
        }

        public async Task<Media> GetById(Guid id)
        {
            var result = await Get(x => x.GetById(Configuration.ProjectAlias, id)).ConfigureAwait(false);
            return result;
        }

        public async Task<T> GetById<T>(Guid id) where T : IMedia
        {
            var result = await GetTyped<T>(x => x.GetById(Configuration.ProjectAlias, id)).ConfigureAwait(false);
            return result;
        }

        public async Task<PagedMedia> GetChildren(Guid id, int page = 0, int pageSize = 10)
        {
            var result = await Get(x => x.GetChildren(Configuration.ProjectAlias, id, page, pageSize)).ConfigureAwait(false);
            return result;
        }

        public async Task<PagedMedia<T>> GetChildren<T>(Guid id, int page = 0, int pageSize = 10) where T : IMedia
        {
            var result = await GetTyped<T>(x => x.GetChildren(Configuration.ProjectAlias, id, page, pageSize)).ConfigureAwait(false);
            return result;
        }

        private async Task<T> Get<T>(Func<MediaDeliveryEndpoints, Task<ApiResponse<T>>> getResponse)
        {
            using (var response = await getResponse(Service).ConfigureAwait(false))
                return GetResponse(response);
        }

        private async Task<T> GetTyped<T>(Func<TypedMediaDeliveryEndpoints<T>, Task<ApiResponse<T>>> getResponse) where T : IMedia
        {
            var service = RestService.For<TypedMediaDeliveryEndpoints<T>>(_httpClient, _refitSettings);

            using (var response = await getResponse(service).ConfigureAwait(false))
                return GetResponse(response);
        }

        private async Task<RootMedia<T>> GetTyped<T>(Func<TypedMediaDeliveryEndpoints<T>, Task<ApiResponse<RootMedia<T>>>> getResponse) where T : IMedia
        {
            var service = RestService.For<TypedMediaDeliveryEndpoints<T>>(_httpClient, _refitSettings);

            using (var response = await getResponse(service).ConfigureAwait(false))
                return GetResponse(response);
        }

        private async Task<PagedMedia<T>> GetTyped<T>(Func<TypedMediaDeliveryEndpoints<T>, Task<ApiResponse<PagedMedia<T>>>> getResponse) where T : IMedia
        {
            var service = RestService.For<TypedMediaDeliveryEndpoints<T>>(_httpClient, _refitSettings);

            using (var response = await getResponse(service).ConfigureAwait(false))
                return GetResponse(response);
        }
    }
}
