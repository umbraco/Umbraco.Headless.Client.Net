using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    internal class ContentService : IContentService
    {
        private readonly IHeadlessConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly RefitSettings _refitSettings;
        private ContentManagementEndpoints _restService;

        public ContentService(IHeadlessConfiguration configuration, HttpClient httpClient, RefitSettings refitSettings)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _refitSettings = refitSettings ?? throw new ArgumentNullException(nameof(refitSettings));
        }

        private ContentManagementEndpoints Service =>
            _restService ??= RestService.For<ContentManagementEndpoints>(_httpClient, _refitSettings);

        public Task<Content> Create(Content content) => Service.Create(_configuration.ProjectAlias, content);

        public Task<Content> Delete(Guid id) => Service.Delete(_configuration.ProjectAlias, id);

        public Task<Content> GetById(Guid id) => Service.ById(_configuration.ProjectAlias, id);

        public Task<PagedContent> GetChildren(Guid id, int page, int pageSize) =>
            Service.Children(_configuration.ProjectAlias, id, page, pageSize);

        public async Task<IEnumerable<Content>> GetRoot()
        {
            var content = await Service.GetRoot(_configuration.ProjectAlias);
            return content.Content.Items;
        }

        public Task<Content> Publish(Guid id, string culture) =>
            Service.Publish(_configuration.ProjectAlias, id, culture);

        public Task<Content> Update(Content content) =>
            Service.Update(_configuration.ProjectAlias, content.Id, content);

        public Task<Content> Unpublish(Guid id, string culture) =>
            Service.Unpublish(_configuration.ProjectAlias, id, culture);
    }
}
