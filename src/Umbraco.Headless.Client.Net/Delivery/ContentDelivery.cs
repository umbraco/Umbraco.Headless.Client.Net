using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Delivery.Models;

namespace Umbraco.Headless.Client.Net.Delivery
{
    internal class ContentDelivery : DeliveryBase, IContentDelivery
    {
        private readonly HttpClient _httpClient;
        private readonly RefitSettings _refitSettings;
        private readonly ModelNameResolver _modelNameResolver;
        private ContentDeliveryEndpoints _service;

        public ContentDelivery(IHeadlessConfiguration configuration, HttpClient httpClient, RefitSettings refitSettings, ModelNameResolver modelNameResolver) : base(configuration)
        {
            _httpClient = httpClient;
            _refitSettings = refitSettings;
            _modelNameResolver = modelNameResolver;
        }

        private ContentDeliveryEndpoints Service =>
            _service ?? (_service = RestService.For<ContentDeliveryEndpoints>(_httpClient, _refitSettings));

        public async Task<IEnumerable<Content>> GetRoot(string culture)
        {
            var result = await Get(x => x.GetRoot(Configuration.ProjectAlias, culture)).ConfigureAwait(false);

            return result?.Content.Items;
        }

        public async Task<IEnumerable<T>> GetRoot<T>(string culture) where T : IContent
        {
            var contentType = GetAliasFromClassName<T>();

            var result = await GetTyped<T>(x => x.GetRoot(Configuration.ProjectAlias, culture, contentType)).ConfigureAwait(false);
            return result?.Content.Items;
        }

        public async Task<Content> GetById(Guid id, string culture, int depth)
        {
            var result = await Get(x => x.GetById(Configuration.ProjectAlias, culture, id, depth)).ConfigureAwait(false);
            return result;
        }

        public async Task<T> GetById<T>(Guid id, string culture, int depth) where T : IContent
        {
            var contentType = GetAliasFromClassName<T>();

            var result = await GetTyped<T>(x => x.GetById(Configuration.ProjectAlias, culture, id, contentType, depth)).ConfigureAwait(false);
            return result;
        }

        public async Task<Content> GetByUrl(string url, string culture, int depth)
        {
            var result = await Get(x => x.GetByUrl(Configuration.ProjectAlias, culture, url, depth)).ConfigureAwait(false);
            return result;
        }

        public async Task<T> GetByUrl<T>(string url, string culture, int depth) where T : IContent
        {
            var contentType = GetAliasFromClassName<T>();

            var result = await GetTyped<T>(x => x.GetByUrl(Configuration.ProjectAlias, culture, url, contentType, depth)).ConfigureAwait(false);
            return result;
        }

        public async Task<PagedContent> GetChildren(Guid id, string culture, int page, int pageSize)
        {
            var result = await Get(x => x.GetChildren(Configuration.ProjectAlias, culture, id, page, pageSize)).ConfigureAwait(false);
            return result;
        }

        public async Task<PagedContent<T>> GetChildren<T>(Guid id, string culture, int page, int pageSize) where T : IContent
        {
            var contentType = GetAliasFromClassName<T>();

            var result = await GetTyped<T>(x => x.GetChildren(Configuration.ProjectAlias, culture, id, contentType, page, pageSize)).ConfigureAwait(false);
            return result;
        }

        public async Task<PagedContent> GetDescendants(Guid id, string culture, int page, int pageSize)
        {
            var result = await Get(x => x.GetDescendants(Configuration.ProjectAlias, culture, id, page, pageSize)).ConfigureAwait(false);
            return result;
        }

        public async Task<PagedContent<T>> GetDescendants<T>(Guid id, string culture, int page, int pageSize) where T : IContent
        {
            var contentType = GetAliasFromClassName<T>();

            var result = await GetTyped<T>(x => x.GetDescendants(Configuration.ProjectAlias, culture, id, contentType, page, pageSize)).ConfigureAwait(false);
            return result;
        }

        public async Task<IEnumerable<Content>> GetAncestors(Guid id, string culture)
        {
            var result = await Get(x => x.GetAncestors(Configuration.ProjectAlias, culture, id)).ConfigureAwait(false);
            return result?.Content.Items;
        }

        public async Task<IEnumerable<T>> GetAncestors<T>(Guid id, string culture) where T : IContent
        {
            var contentType = GetAliasFromClassName<T>();

            var result = await GetTyped<T>(x => x.GetAncestors(Configuration.ProjectAlias, culture, id, contentType)).ConfigureAwait(false);
            return result?.Content.Items;
        }

        public async Task<PagedContent> GetByType(string contentType, string culture = null, int page = 1, int pageSize = 10)
        {
            var result = await Get(x => x.GetByType(Configuration.ProjectAlias, culture, contentType, page, page)).ConfigureAwait(false);
            return result;
        }

        public async Task<PagedContent<T>> GetByType<T>(string culture = null, int page = 1, int pageSize = 10) where T : IContent
        {
            var contentType = GetAliasFromClassName<T>();
            return await GetByTypeAlias<T>(contentType, culture, page, pageSize).ConfigureAwait(false);
        }

        public async Task<PagedContent<T>> GetByTypeAlias<T>(string contentTypeAlias, string culture = null, int page = 1, int pageSize = 10) where T : IContent
        {
            var contentType = GetAliasFromClassName<T>();

            var result = await GetTyped<T>(x => x.GetByType(Configuration.ProjectAlias, culture, contentType, page, pageSize)).ConfigureAwait(false);
            return result;
        }

        public async Task<PagedContent<T>> Filter<T>(ContentFilter filter, string culture = null, int page = 1, int pageSize = 10) where T : IContent
        {
            if(filter == null || filter.Properties.Length == 0)
                throw new ArgumentException("ContentFilter should contain at least one property to filter on");

            filter.ContentTypeAlias = filter.ContentTypeAlias ?? GetAliasFromClassName<T>();

            var result = await GetTyped<T>(x => x.Filter(Configuration.ProjectAlias, culture, filter, page, pageSize)).ConfigureAwait(false);
            return result;
        }

        public async Task<PagedContent> Filter(ContentFilter filter, string culture = null, int page = 1, int pageSize = 10)
        {
            if(filter == null || filter.Properties.Length == 0)
                throw new ArgumentException("ContentFilter should contain at least one property to filter on");

            var result = await Get(x => x.Filter(Configuration.ProjectAlias, culture, filter, page, page)).ConfigureAwait(false);
            return result;
        }

        public async Task<PagedContent> Search(string term, string culture = null, int page = 1, int pageSize = 10)
        {
            var result = await Get(x => x.Search(Configuration.ProjectAlias, culture, term, page, pageSize)).ConfigureAwait(false);
            return result;
        }

        private async Task<T> Get<T>(Func<ContentDeliveryEndpoints, Task<ApiResponse<T>>> getResponse)
        {
            using (var response = await getResponse(Service).ConfigureAwait(false))
                return GetResponse(response);
        }

        private async Task<T> GetTyped<T>(Func<TypedContentDeliveryEndpoints<T>, Task<ApiResponse<T>>> getResponse) where T : IContent
        {
            var service = RestService.For<TypedContentDeliveryEndpoints<T>>(_httpClient, _refitSettings);

            using (var response = await getResponse(service).ConfigureAwait(false))
                return GetResponse(response);
        }

        private async Task<RootContent<T>> GetTyped<T>(Func<TypedContentDeliveryEndpoints<T>, Task<ApiResponse<RootContent<T>>>> getResponse) where T : IContent
        {
            var service = RestService.For<TypedContentDeliveryEndpoints<T>>(_httpClient, _refitSettings);

            using (var response = await getResponse(service).ConfigureAwait(false))
                return GetResponse(response);
        }

        private async Task<PagedContent<T>> GetTyped<T>(Func<TypedContentDeliveryEndpoints<T>, Task<ApiResponse<PagedContent<T>>>> getResponse) where T : IContent
        {
            var service = RestService.For<TypedContentDeliveryEndpoints<T>>(_httpClient, _refitSettings);

            using (var response = await getResponse(service).ConfigureAwait(false))
                return GetResponse(response);
        }

        private string GetAliasFromClassName<T>() => _modelNameResolver.GetContentModelAlias(typeof(T));
    }
}
