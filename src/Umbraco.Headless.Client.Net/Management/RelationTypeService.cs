using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Management.Models;

namespace Umbraco.Headless.Client.Net.Management
{
    internal class RelationTypeService : IRelationTypeService
    {
        private readonly IHeadlessConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly RefitSettings _refitSettings;
        private RelationTypeManagementEndpoints _restService;

        public RelationTypeService(IHeadlessConfiguration configuration, HttpClient httpClient,
            RefitSettings refitSettings)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _refitSettings = refitSettings ?? throw new ArgumentNullException(nameof(refitSettings));
        }

        private RelationTypeManagementEndpoints Service =>
            _restService ?? (_restService = RestService.For<RelationTypeManagementEndpoints>(_httpClient, _refitSettings));

        public async Task<IEnumerable<RelationType>> GetAll()
        {
            var root = await Service.GetRoot(_configuration.ProjectAlias).ConfigureAwait(false);
            return root.RelationTypes.Items;
        }

        public async Task<RelationType> GetByAlias(string alias) =>
            await Service.ByAlias(_configuration.ProjectAlias, alias).ConfigureAwait(false);
    }
}
