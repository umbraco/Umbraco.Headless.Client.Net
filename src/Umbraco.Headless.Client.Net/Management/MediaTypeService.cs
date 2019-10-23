using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    internal class MediaTypeService : IMediaTypeService
    {
        private readonly IHeadlessConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly RefitSettings _refitSettings;
        private MediaTypeManagementEndpoints _restService;

        public MediaTypeService(IHeadlessConfiguration configuration, HttpClient httpClient,
            RefitSettings refitSettings)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _refitSettings = refitSettings ?? throw new ArgumentNullException(nameof(refitSettings));
        }

        private MediaTypeManagementEndpoints Service =>
            _restService ??= RestService.For<MediaTypeManagementEndpoints>(_httpClient, _refitSettings);

        public async Task<IEnumerable<MediaType>> GetAll()
        {
            var root = await Service.GetRoot(_configuration.ProjectAlias);
            return root.MediaTypes.Items;
        }

        public async Task<MediaType> GetByAlias(string alias) =>
            await Service.ByAlias(_configuration.ProjectAlias, alias);
    }
}
