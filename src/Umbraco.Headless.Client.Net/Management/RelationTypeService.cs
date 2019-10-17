using System;
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
        private RelationTypeManagementEndpoints _restService;

        public RelationTypeService(IHeadlessConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        private RelationTypeManagementEndpoints Service =>
            _restService ?? (_restService = RestService.For<RelationTypeManagementEndpoints>(_httpClient));

        public async Task<RelationType> GetByAlias(string alias) =>
            await Service.ByAlias(_configuration.ProjectAlias, alias);
    }
}
